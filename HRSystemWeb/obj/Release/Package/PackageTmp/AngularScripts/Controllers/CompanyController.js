
app.controller('CompanyController', ['$scope', 'i18nService', '$http', function ($scope, i18nService, $http) {
    i18nService.setCurrentLang('ru');
   

    $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
    if (col.filters[0].term) {
        return 'header-filtered';
    } else {
        return '';
    }
};


    $scope.deleteRow = function (row) {
        swal({
            title: "Вы уверены?", text: "После удаления компания " + row.entity.Name+ " станет недоступна!",cancelButtonText:"Отмена", type: "warning", showCancelButton: true,
            confirmButtonColor: "#DD6B55", confirmButtonText: "Да, удалить!", closeOnConfirm: false
        }, function() {
            
            $http({
                url: 'Company/DeleteCompany',
                method: "GET",
                params: { id: row.entity.ID }
            }).success(function (data) {

                if (data.success) {
                    var index = $scope.gridOptionsCompanies.data.indexOf(row.entity);
                    $scope.gridOptionsCompanies.data.splice(index, 1);
                    swal("Удалено!", "Компания "+row.entity.Name+" была успешно удалена.", "success");
                } else {
                    swal("Oops", "Не удалось удалить", "warning");

                }

            });
            
        });
    };

    $scope.edit = function (row) {
       // row.entity.data enableCellEdit = true;
        swal({ title: "Редактирование", text: "Введите наименование", type: "input", showCancelButton: true, closeOnConfirm: true, animation: "slide-from-top",  inputValue: row.entity.Name },
            function (inputValue) {
                if (inputValue === false) return false; if (inputValue === "") { swal.showInputError("Вы ничего не ввели"); return false }

               
                $http({
                    url: 'Company/UpdateCompany',
                    method: "GET",
                    params: {id:row.entity.ID, name:row.entity.Name}
                }).success(function() {
                    row.entity.Name = inputValue;
                    return true;
                });
                return false;

            });
    };



    $scope.gridOptionsCompanies = {
        paginationPageSizes: [10, 20, 30],
        enableGridMenu: true,
        enableFiltering: true,
        paginationPageSize: 10,
        columnDefs: [
        {
            name: ' ',
            cellTemplate: '<center><button type="button" ng-click="grid.appScope.deleteRow(row)" class="btn btn-default"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span></button> <button type="button" ng-click="grid.appScope.edit(row)" class="btn btn-default"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></button></center>',
            width: 140,
            enableSorting: false,
            enableFiltering: false
        },
          { name: 'Name', displayName: 'Компания', headerCellClass: $scope.highlightFilteredHeader }
        ],
        data: {}
     //   gridMenuCustomItems: [
     //{
     //    title: 'Добавить',
     //    action: function ($event) {
             
     //    },
     //    order: 210
     //}
      //  ]
    };
   
    $scope.getCompanies = function() {
        $http({
            url: 'Company/GetCompanies',
            method: "GET",
            params: { filter: "", sortColumn: "", sortDirection: "", page: 1, pageSize: $scope.gridOptionsCompanies.paginationPageSize }
        }).success(function(returnedData) {
            $scope.gridOptionsCompanies.data = JSON.parse(returnedData.jsonModel);
        });
    };
    
    $scope.addCompany = function () {
        if ($scope.Name !== "") {
            $http({
                url: 'Company/AddCompany',
                method: "GET",
                params: { name: $scope.Name }
            }).success(function(data) {

                if (data.success) {
                    $scope.gridOptionsCompanies.data.push(JSON.parse(data.jsonModel));
                    $scope.Name = "";
                    swal("Отлично!", "Компания успешно добавлена", "success");

                } else {
                    swal(JSON.parse(data.jsonModel));
                    $scope.Name = "";
                }

            });
        }
        else
            swal("Oops", "Введите наименование компании", "warning");
    };
    $scope.gridOptionsCompanies.onRegisterApi = function (gridApi) {
        $scope.gridApi = gridApi;
    };
}]);