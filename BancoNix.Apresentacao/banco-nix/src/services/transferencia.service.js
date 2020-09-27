import config from '../common/config';

export default {
  name: 'transferenciaService',
  factory: ['$http', ($http) => {
    function buscar() {
      return $http.get(config.api.base + config.api.resources.transferencias)
        .then(result => result.data)
        .catch(error => error);
    }

    function buscarComFiltro(filtro) {
      const filters = [];

      if (filtro.data)
        filters.push('dt=' + filtro.data.toISOString().slice(0,10));

      if (filtro.nomePagador)
        filters.push('np=' + filtro.nomePagador);

      if (filtro.nomeBeneficiario)
        filters.push('nb=' + filtro.nomeBeneficiario);

      if (filtro.status)
        filters.push('st=' + filtro.status);

      if (filtro.tipo)
        filters.push('tp=' + filtro.tipo);

      var url = config.api.base + config.api.resources.transferencias;

      for (var i = 0; i < filters.length; i++) {
        if (i == 0)
          url += `?${filters[i]}`;
        else
          url += `&${filters[i]}`;
      }

      return $http.get(url)
        .then(result => result.data)
        .catch(error => error);
    }

    function deletar(id){
      return $http.delete(config.api.base + config.api.resources.transferencias + `${id}`)
        .then(result => result.data)
        .catch(error => error);
    }

    return {
      buscar,
      buscarComFiltro,
      deletar
    };
  }]
}