using System;
using System.Collections.Generic;

namespace Jequiti.Aplicacao.Infra.Seguranca
{
    public static class Senha
    {
        public static string GerarSenhaCliente()
        {
            var _random = new Random();
            var tamanho_minimo = 6;
            var tamanho_maximo = 8;
            var tamanho_senha = _random.Next(tamanho_minimo, tamanho_maximo + 1);
            var fonte = new List<string>();

            for (int i = 0; i < 26; i++)
            {
                // Não irá gerar a senha com as letras H, K, O, W, Y 
                if (i != 7 && i != 10 && i != 14 && i != 22 && i != 24)
                {
                    fonte.Add(((char)('a' + i)).ToString());
                }
            }

            var senha = string.Empty;

            for (int i = 0; i < tamanho_senha; i++)
            {
                senha += (_random.Next(0, 2) == 0) ?
                    fonte[_random.Next(0, 21)] :
                    _random.Next(1, 9).ToString();
            }

            return senha;
        }
    }
}
