using BancoNix.Aplicacao.Commands;
using BancoNix.Aplicacao.Filtros;
using BancoNix.Aplicacao.Models;
using BancoNix.Dominio;
using BancoNix.Infra.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BancoNix.Aplicacao
{
    public class TransferenciaService : ITransferenciaService
    {
        private readonly BancoContext _context;

        public TransferenciaService(BancoContext context)
        {
            _context = context;
        }

        public async Task<ResultadoTransferenciaModel> Buscar(FiltroTransferencia filtro)
        {
            ResultadoTransferenciaModel resultado = new ResultadoTransferenciaModel();

            var t = _context.Transferencias.Where(x => !x.Removida).AsQueryable();

            if (!string.IsNullOrEmpty(filtro.NomePagador))
            {
                t = t.Where(x => x.Pagador.Nome.Equals(filtro.NomePagador));
            }

            if (!string.IsNullOrEmpty(filtro.NomeBeneficiario))
            {
                t = t.Where(x => x.Beneficiario.Nome.Equals(filtro.NomeBeneficiario));
            }

            if (filtro.Data != default)
            {
                t = t.Where(x => x.Data.Date == filtro.Data.Value.Date);
            }

            if (filtro.Status != null)
            {
                t = t.Where(x => x.Status.Equals(filtro.Status));
            }

            if (filtro.Tipo != null)
            {
                t = t.Where(x => x.Tipo.Equals(filtro.Tipo));
            }

            var dbTransferencias = await t.ToListAsync();

            resultado.Somatoria = dbTransferencias.Sum(x => x.Valor);
            resultado.Quantidade = dbTransferencias.Count;

            resultado.Transferencias = dbTransferencias
                .Select(x => new TransferenciaModel
                {
                    Id = x.Id,
                    Data = x.Data,
                    Status = x.Status,
                    Tipo = x.Tipo,
                    Valor = x.Valor,
                    Beneficiario = new DadosTransacaoModel(x.Beneficiario.Nome, x.Beneficiario.Banco, x.Beneficiario.Agencia, x.Beneficiario.Conta),
                    Pagador = new DadosTransacaoModel(x.Pagador.Nome, x.Pagador.Banco, x.Pagador.Agencia, x.Pagador.Conta),
                    Usuario = new UsuarioModel(x.Usuario.Id, x.Usuario.Nome, x.Usuario.Cnpj)
                })
                .ToList();

            return resultado;
        }

        public async Task<TransferenciaModel> Buscar(int id)
        {
            TransferenciaModel resultado = new TransferenciaModel();

            var transferencia = await _context.Transferencias.FirstOrDefaultAsync(t => t.Id == id && !t.Removida);

            return transferencia is null ? null : new TransferenciaModel
            {
                Id = transferencia.Id,
                Data = transferencia.Data,
                Status = transferencia.Status,
                Tipo = transferencia.Tipo,
                Valor = transferencia.Valor,
                Beneficiario = new DadosTransacaoModel(transferencia.Beneficiario.Nome, transferencia.Beneficiario.Banco, transferencia.Beneficiario.Agencia, transferencia.Beneficiario.Conta),
                Pagador = new DadosTransacaoModel(transferencia.Pagador.Nome, transferencia.Pagador.Banco, transferencia.Pagador.Agencia, transferencia.Pagador.Conta),
                Usuario = new UsuarioModel(transferencia.Usuario.Id, transferencia.Usuario.Nome, transferencia.Usuario.Cnpj)
            };
        }

        public async Task<bool> Criar(CriarTransferenciaCommand command)
        {
            DadosTransacao pagador = new DadosTransacao(command.Pagador.Nome, command.Pagador.Banco, command.Pagador.Agencia, command.Pagador.Conta);

            DadosTransacao beneficiario = new DadosTransacao(command.Beneficiario.Nome, command.Beneficiario.Banco, command.Beneficiario.Agencia, command.Beneficiario.Conta);

            Transferencia nova = new Transferencia(command.UsuarioId, pagador, beneficiario, command.Valor);

            await _context.Transferencias.AddAsync(nova);

            var registros = await _context.SaveChangesAsync();

            return registros > 0;
        }

        public async Task<bool> Remover(int id)
        {
            var transferencia = await _context.Transferencias.FirstOrDefaultAsync(t => t.Id == id);
            
            if (transferencia != null)
                transferencia.Removida = true;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
