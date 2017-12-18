using AutoMapper;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades;
using SistemaDeVendas.Aplicacao.Infraestrutura;
using SistemaDeVendas.Aplicacao.Seguranca;
using System;
using System.Linq;
using System.Text;

namespace SistemaDeVendas.Aplicacao.Servicos
{
    public class ServicoCliente
    {
        private Contexto contexo = new Contexto();
        private ServicoUsuario _servicoUsuario;

        public ServicoCliente(ServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

        public string Cadastrar(ClienteDto clienteDto)
        {
            if (clienteDto == null)
            {
                throw new ArgumentException(nameof(clienteDto));
            }

            var cliente = Mapper.Map<ClienteDto, Cliente>(clienteDto);
            ChaveAssimetrica.GenKey_SaveInContainer(clienteDto.Cpf);
            var chavePublica = ChaveAssimetrica.GetKeyPublicFromContainer(clienteDto.Cpf);
            var usuario = _servicoUsuario.GerarUsuario(cliente);

            cliente.NumeroCartao = ChaveAssimetrica.RSAEncrypt(Encoding.Unicode.GetBytes(clienteDto.NumeroCartao), chavePublica);
            cliente.CodigoSeguranca = ChaveAssimetrica.RSAEncrypt(Encoding.Unicode.GetBytes(clienteDto.CodigoSeguranca), chavePublica);
            cliente.Usuario = usuario.Item2;
            contexo.Clientes.Add(cliente);
            contexo.SaveChanges();
            return usuario.Item1;
        }

        public ClienteDto ObterCliente(int id)
        {
            var cliente = contexo.Clientes.Where(c => c.Id == id).FirstOrDefault();
            var chavePrivada = ChaveAssimetrica.GetKeyPrivateFromContainer(cliente.Cpf);

            if (cliente != null)
            {
                var clienteDto = Mapper.Map<Cliente, ClienteDto>(cliente);

                clienteDto.NumeroCartao = Encoding.Unicode.GetString(ChaveAssimetrica.RSADecrypt(cliente.NumeroCartao, chavePrivada));
                clienteDto.CodigoSeguranca = Encoding.Unicode.GetString(ChaveAssimetrica.RSADecrypt(cliente.CodigoSeguranca,chavePrivada));

                return clienteDto;
            }

            return null;
        }
    }
}
