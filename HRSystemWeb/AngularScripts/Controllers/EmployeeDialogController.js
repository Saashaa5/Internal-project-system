angular
.module('app')
.controller('EmployeeDialogController', function ($scope, $mdDialog, $http, item, companies) {
    // items is injected in the controller, not its scope!
    $scope.items = item;
    $scope.companies = companies;

    $scope.cancel = function () {

        $mdDialog.cancel();

    };
    $scope.upadteEmployee = function () {

        $http({
            url: 'Employee/UpdateEmployee',
            method: "GET",
            params: {
                newEmployee: $scope.items
            }
        }).success(function (companyName) {
            $scope.items.Company.Name = companyName.companyName;
            $mdDialog.cancel();
        });

    };
});