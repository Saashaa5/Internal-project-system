app.controller('EmployeeController', [
    '$scope', 'i18nService', '$http', '$mdDialog', function ($scope, i18nService, $http, $mdDialog) {
        i18nService.setCurrentLang('ru');
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };

        $scope.gridOptionsEmployees = {
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
              { name: 'Name', displayName: 'Имя', headerCellClass: $scope.highlightFilteredHeader },
              { name: 'Surname', displayName: 'Фамилия', headerCellClass: $scope.highlightFilteredHeader },
              { name: 'Patronymic', displayName: 'Отчество', headerCellClass: $scope.highlightFilteredHeader },
              { name: 'Email', displayName: 'Почта', headerCellClass: $scope.highlightFilteredHeader },
              { name: 'Company.Name', displayName: 'Компания', headerCellClass: $scope.highlightFilteredHeader }


            ],
            data: {}
          };

        $http({
            url: 'Company/GetCompanies',
            method: "GET",
            params: { filter: "", sortColumn: "", sortDirection: "", page: 1, pageSize: $scope.gridOptionsEmployees.paginationPageSize }
        }).success(function (returnedData) {
            $scope.companies = JSON.parse(returnedData.jsonModel);
        });

        $scope.getEmployees = function () {
            $http({
                url: 'Employee/GetEmployees',
                method: "GET",
                params: { filter: "", sortColumn: "", sortDirection: "", page: 1, pageSize: $scope.gridOptionsEmployees.paginationPageSize }
            }).success(function (returnedData) {
                $scope.gridOptionsEmployees.data = JSON.parse(returnedData.jsonModel);
            });
        };
        $scope.addEmployee = function () {
            if ($scope.Name !== "" && $scope.Surname !== "" && $scope.Email !== "") {

                $http({
                    url: 'Employee/AddEmployee',
                    method: "GET",
                    params: { name: $scope.Name, surname: $scope.Surname, patronymic: $scope.Patronymic, email: $scope.Email, companyId: $scope.selectedCompany }
                }).success(function (returnedData) {
                    if (returnedData.success) {
                        $scope.gridOptionsEmployees.data.push(JSON.parse(returnedData.jsonModel));
                        $scope.Name = "";
                        $scope.Surname = "";
                        $scope.Patronymic = "";
                        $scope.Email = "";
                        swal("Отлично!", "Сотрудник успешно добавлен", "success");

                    } else {

                        $scope.Name = "";
                        $scope.Surname = "";
                        $scope.Patronymic = "";
                        $scope.Email = "";
                        swal(JSON.parse(returnedData.jsonModel));
                    }


                });
            }
        };
        $scope.edit = function (row) {
            $mdDialog.show({
                controller: 'EmployeeDialogController',
                templateUrl: 'Home/EmployeeModalDialog',
                targetEvent: row,
                locals: {
                    item: row.entity,
                    companies: $scope.companies
                },
                clickOutsideToClose: true
            });
        };

        $scope.deleteRow = function (row) {
            swal({
                title: "Вы уверены?", text: "После удаления сотрудник " + row.entity.Name + " станет недоступен!", cancelButtonText: "Отмена", type: "warning", showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Да, удалить!", closeOnConfirm: false
            }, function () {

                $http({
                    url: 'Employee/DeleteEmployee',
                    method: "GET",
                    params: { id: row.entity.ID }
                }).success(function (data) {

                    if (data.success) {
                        var index = $scope.gridOptionsEmployees.data.indexOf(row.entity);
                        $scope.gridOptionsEmployees.data.splice(index, 1);
                        swal("Удалено!", "Сотрудник " + row.entity.Name + " был успешно удален.", "success");
                    } else {
                        swal("Oops", "Не удалось удалить", "warning");

                    }

                });

            });
        };


    }]);