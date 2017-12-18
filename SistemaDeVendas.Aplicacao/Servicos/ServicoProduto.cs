using AutoMapper;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades;
using SistemaDeVendas.Aplicacao.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendas.Aplicacao.Servicos
{
    public class ServicoProduto
    {
        private Contexto contexo = new Contexto();

        public void Cadastrar(ProdutoDto produtoDto)
        {
            if (produtoDto == null)
            {
                throw new ArgumentException(nameof(produtoDto));
            }

            var produto = Mapper.Map<ProdutoDto, Produto>(produtoDto);
           
            contexo.Produtos.Add(produto);
            contexo.SaveChanges();
        }

        public void Atualizar(ProdutoDto produtoDto)
        {
            if (produtoDto == null)
            {
                throw new ArgumentException(nameof(produtoDto));
            }

            var produto = contexo.Produtos.Where(p => p.Id == produtoDto.Id).FirstOrDefault();

            if(produto != null)
            {
                produto.Descricao = produtoDto.Descricao;
                produto.UnidadeMedida = produtoDto.UnidadeMedida;
                produto.ValorUnidade = produtoDto.ValorUnidade;
                contexo.Produtos.AddOrUpdate(produto);
                contexo.SaveChanges();
            }            
        }


        public ProdutoDto Obter(int id)
        {
            var produto = contexo.Produtos.Where(u => u.Id == id).FirstOrDefault();

            return Mapper.Map<Produto, ProdutoDto>(produto);
        }

        public List<ProdutoDto> Obtertodos()
        {
            var produtos = contexo.Produtos.ToList();

            return Mapper.Map<List<Produto>, List<ProdutoDto>>(produtos);
        }

    }
}
