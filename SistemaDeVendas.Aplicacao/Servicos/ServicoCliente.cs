using AutoMapper;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades;
using SistemaDeVendas.Aplicacao.Infraestrutura;
using SistemaDeVendas.Aplicacao.Seguranca;
using SistemaDeVendas.Aplicacao.Util;
using System;
using System.Collections.Generic;
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

            if (Password.VerificarSenha(clienteDto.Senha) < PasswordScore.Strong)
            {
                throw new ArgumentException("Senha muito fraca");
            }

            try
            {
                var cliente = Mapper.Map<ClienteDto, Cliente>(clienteDto);
                ChaveAssimetrica.GenKey_SaveInContainer(clienteDto.Cpf);
                var chavePublica = ChaveAssimetrica.GetKeyPublicFromContainer(clienteDto.Cpf);
                var usuario = _servicoUsuario.GerarUsuario(cliente, clienteDto.Senha);

                cliente.NumeroCartao = ChaveAssimetrica.RSAEncrypt(Encoding.Unicode.GetBytes(clienteDto.NumeroCartao), chavePublica);
                cliente.CodigoSeguranca = ChaveAssimetrica.RSAEncrypt(Encoding.Unicode.GetBytes(clienteDto.CodigoSeguranca), chavePublica);
                cliente.Usuario = usuario.Item2;
                contexo.Clientes.Add(cliente);
                contexo.SaveChanges();
                return usuario.Item1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public ClienteDto ObterCliente(int id)
        {
            var cliente = contexo.Clientes.Where(c => c.Id == id).FirstOrDefault();

            if (cliente != null)
            {
                var chavePrivada = ChaveAssimetrica.GetKeyPrivateFromContainer(cliente.Cpf);

                var clienteDto = Mapper.Map<Cliente, ClienteDto>(cliente);

                clienteDto.NumeroCartao = Encoding.Unicode.GetString(ChaveAssimetrica.RSADecrypt(cliente.NumeroCartao, chavePrivada));
                clienteDto.CodigoSeguranca = Encoding.Unicode.GetString(ChaveAssimetrica.RSADecrypt(cliente.CodigoSeguranca, chavePrivada));

                return clienteDto;
            }

            return null;
        }


        public ClienteDto ObterClientePorUsuario(int id, string chave)
        {
            var cliente = contexo.Clientes.Where(c => c.Usuario.Id == id).FirstOrDefault();

            if (cliente != null)
            {
                var clienteDto = Mapper.Map<Cliente, ClienteDto>(cliente);

                clienteDto.NumeroCartao = Encoding.Unicode.GetString(ChaveAssimetrica.RSADecrypt(cliente.NumeroCartao, chave));
                clienteDto.CodigoSeguranca = Encoding.Unicode.GetString(ChaveAssimetrica.RSADecrypt(cliente.CodigoSeguranca, chave));

                return clienteDto;
            }

            return null;
        }

        public List<ClienteDto> ObterTodos()
        {
            var clientes = contexo.Clientes.Where(c => 1 == 1).ToList();


            return Mapper.Map<List<Cliente>, List<ClienteDto>>(clientes);
        }


    }
}
