import angular from 'angular';
import template from './custom-table.html';
import './custom-table.scss';
import transferenciaService from '../../services/transferencia.service';

export default angular
  .module('customTable.component', ['ui.grid', 'ui.grid.pagination', 'ui.grid.resizeColumns', 'ui.grid.selection'])
  .factory(transferenciaService.name, transferenciaService.factory)
  .component('customTable', {
    template,
    controller: [transferenciaService.name, '$scope', '$location', function (transferencias, $scope) {
      const ctrl = this;

      $scope.filter = {};
      $scope.vendoDetalhes = false;

      $scope.linhaSelecionada = undefined;

      $scope.gridOptions = {
        paginationPageSizes: [10, 25, 50],
        paginationPageSize: 10,
        multiSelect: false,
        columnDefs: [
          { name: 'beneficiario.nome', displayName: 'Beneficiario' },
          //{ name: 'beneficiario.banco' },
          //{ name: 'beneficiario.agencia' },
          //{ name: 'beneficiario.conta' },
          { name: 'pagador.nome', displayName: 'Pagador' },
          //{ name: 'pagador.banco' },
          //{ name: 'pagador.agencia' },
          //{ name: 'pagador.conta' },
          { name: 'valor' },
          { name: 'tipo' },
          { name: 'status'},
          { name: 'data', type: 'date', cellFilter: 'date:\'dd/MM/yyyy\''}
        ]
      };

      $scope.gridOptions.onRegisterApi = function (gridApi) {
        $scope.gridApi = gridApi;
        gridApi.selection.on.rowSelectionChanged($scope,function(row){
          $scope.linhaSelecionada = row.isSelected ? row.entity : undefined;
        });
      }

      $scope.filtroAlterado = function () {
        transferencias.buscarComFiltro($scope.filter)
          .then(result => {
            $scope.somatoria = result.somatoria;
            $scope.gridOptions.data = result.transferencias;
          })
          .catch(error => {
            ctrl.transferencias = error;
          });
      }

      $scope.deletar = function(){
        
        transferencias.deletar($scope.linhaSelecionada.id)
        .then(() => {
          alert('Transferencia excluida com sucesso');

          $scope.filtroAlterado();
        })
        .catch(error => {
          alert('Não foi possível excluir a transferencia. Erro: ' + error)
        });
      }

      $scope.verDetalhes = function(){
        $scope.vendoDetalhes = true;

      }

      $scope.filtroAlterado();      
    }],
  })
  .name;
