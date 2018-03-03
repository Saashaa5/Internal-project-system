app.controller('ProjectController', [
    '$scope', 'i18nService', '$http', '$mdDialog', function ($scope, i18nService, $http, $mdDialog) {
        i18nService.setCurrentLang('ru');
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };

        $scope.gridOptionsProjects = {
            paginationPageSizes: [10, 20, 30],
            enableGridMenu: true,
            enableFiltering: true,
            paginationPageSize: 10,
            columnDefs: [
                {
                    name: ' ',
                    cellTemplate: '<center><button type="button" ng-click="grid.appScope.deleteRow(row)" class="btn btn-default"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span></button> <button type="button" ng-click="grid.appScope.edit(row)" class="btn btn-default"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></button> <button type="button" ng-click="grid.appScope.manageEmployees(row)" class="btn btn-default"><span class="glyphicon glyphicon-user" aria-hidden="true"></span></button></center>',
                    width: 140,
                    enableSorting: false,
                    enableFiltering: false
                },
                { name: 'Name', displayName: 'Имя', headerCellClass: $scope.highlightFilteredHeader },
                { name: 'StartDate', displayName: 'Дата начала', headerCellClass: $scope.highlightFilteredHeader },
                { name: 'EndDate', displayName: 'Дата конца', headerCellClass: $scope.highlightFilteredHeader },
                { name: 'Priority', displayName: 'Приоритет', headerCellClass: $scope.highlightFilteredHeader },
                { name: 'Comment', displayName: 'Комментарии', headerCellClass: $scope.highlightFilteredHeader },
                { name: 'ClientCompany.Name', displayName: 'Компания Заказчик', headerCellClass: $scope.highlightFilteredHeader },
                { name: 'ExecutorCompany.Name', displayName: 'Компания Исполнитель', headerCellClass: $scope.highlightFilteredHeader }
                
            ],
            data: {}
        };

        $http({
            url: 'Company/GetCompanies',
            method: "GET",
            params: { filter: "", sortColumn: "", sortDirection: "", page: 1, pageSize: $scope.gridOptionsProjects.paginationPageSize }
        }).success(function (returnedData) {
            $scope.companies = JSON.parse(returnedData.jsonModel);
        });

        $http({
            url: 'Employee/GetEmployees',
            method: "GET",
            params: { filter: "", sortColumn: "", sortDirection: "", page: 1, pageSize: $scope.gridOptionsProjects.paginationPageSize }
        }).success(function (returnedData) {
            $scope.employees = JSON.parse(returnedData.jsonModel);
        });

        $scope.manageEmployees = function (row) {

            $mdDialog.show({
                controller: 'ManageProjectEmployees',
                templateUrl: 'Home/AddProjectEmployees',
                targetEvent: row,
                locals: {
                    item: row.entity,
                    employees: $scope.employees

                },
                clickOutsideToClose: true
            });
        };

        $scope.deleteRow = function (row) {
            swal({
                title: "Вы уверены?", text: "После удаления проект " + row.entity.Name + " станет недоступен!", cancelButtonText: "Отмена", type: "warning", showCancelButton: true,
                confirmButtonColor: "#DD6B55", confirmButtonText: "Да, удалить!", closeOnConfirm: false
            }, function () {

                $http({
                    url: 'Project/DeleteProject',
                    method: "GET",
                    params: { id: row.entity.ID }
                }).success(function (data) {

                    if (data.success) {
                        var index = $scope.gridOptionsProjects.data.indexOf(row.entity);
                        $scope.gridOptionsProjects.data.splice(index, 1);
                        swal("Удалено!", "Проект " + row.entity.Name + " был успешно удален.", "success");
                    } else {
                        swal("Oops", "Не удалось удалить", "warning");

                    }

                });

            });
        };

        $scope.edit = function (row) {
            // row.entity.data enableCellEdit = true;
            $mdDialog.show({
                controller: 'ProjectDialogController',
                templateUrl: 'Home/ProjectModalDialog',
                targetEvent: row,
                locals: {
                    item: row.entity,
                    companies: $scope.companies
                },
                clickOutsideToClose: true
                // fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
            });
        };

        $scope.getProjects = function () {
            $http({
                url: 'Project/GetProjects',
                method: "GET",
                params: { filter: "", sortColumn: "", sortDirection: "", page: 1, pageSize: $scope.gridOptionsProjects.paginationPageSize }
            }).success(function (returnedData) {
                $scope.gridOptionsProjects.data = JSON.parse(returnedData.jsonModel);
            });
        };


        $scope.addProject = function () {
            if ($scope.Name !== "" && $scope.Priority !== "" && $scope.StartDate !== "" && $scope.EndDate !== "" && $scope.ClientCompanyId !== "" && $scope.ExecutorCompanyId !== "" && $scope.ChiefId !== "") {

                $http({
                    url: 'Project/AddProject',
                    method: "GET",
                    params: { name: $scope.Name, priority: $scope.Priority, startDate: $scope.StartDate, endDate: $scope.EndDate, clientCompanyId: $scope.ClientCompanyId, comment: $scope.Comment, executorCompanyId: $scope.ExecutorCompanyId }
                }).success(function (returnedData) {
                    if (returnedData.success) {
                        $scope.gridOptionsProjects.data.push(JSON.parse(returnedData.jsonModel));
                        $scope.Name = "";
                        $scope.Priority = "";
                        $scope.StartTime = "";
                        $scope.EndTime = "";
                        $scope.ClientCompanyId = "";
                        $scope.Comment = "";
                        $scope.ExecutorCompanyId = "";
                        swal("Отлично!", "Компания успешно добавлена", "success");

                    } else {

                        $scope.Name = "";
                        $scope.Priority = "";
                        $scope.StartTime = "";
                        $scope.EndTime = "";
                        $scope.ClientCompanyId = "";
                        $scope.Comment = "";
                        $scope.ExecutorCompanyId = "";
                        swal(JSON.parse(returnedData.jsonModel));
                    }


                });
            }
        };


    }]);