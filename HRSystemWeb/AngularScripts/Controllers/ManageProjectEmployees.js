angular
.module('app')
.controller('ManageProjectEmployees', function ($scope, $mdDialog, $http, item, employees) {
    $scope.items = item;
    $scope.employees = employees;
    $http({
        url: 'Project/GetEmployees',
        method: "GET",
        params: {
            projectId: item.ID,
            isChief: false
        }
    }).success(function (answer) {
        $scope.items.EmployeesToProject = answer.employees;
    });

    $scope.cancel = function () {

        $mdDialog.cancel();

    };
    $http({
        url: 'Project/GetEmployees',
        method: "GET",
        params: {
            projectId: item.ID,
            isChief: true
        }
    }).success(function (answer) {
        $scope.items.Chief = answer.employees[0];
    });

    $scope.cancel = function () {

        $mdDialog.cancel();

    };
    $scope.addEmployees = function () {
        $http({
            url: 'Project/AddEmployees',
            method: "GET",
            params: {
                id: $scope.items.ID,
                employees: $scope.items.EmployeesToProject,
                chief: $scope.items.Chief
            }
        }).success(function (answer) {
            if (!answer.success) {
                swal("Oops", "Вы не можете добавлять одного и того же человека в две должности");
            }
            else
                $mdDialog.cancel();
        });

    };
});