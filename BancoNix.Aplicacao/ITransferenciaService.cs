using BancoNix.Aplicacao.Commands;
using BancoNix.Aplicacao.Filtros;
using BancoNix.Aplicacao.Models;
using System.Threading.Tasks;

namespace BancoNix.Aplicacao
{
    public interface ITransferenciaService
    {
        Task<bool> Criar(CriarTransferenciaCommand command);
        Task<bool> Remover(int id);
        Task<ResultadoTransferenciaModel> Buscar(FiltroTransferencia filtro);
        Task<TransferenciaModel> Buscar(int id);

    }
}
