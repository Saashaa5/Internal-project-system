angular
.module('app')
.controller('ProjectDialogController', function ($scope, $mdDialog, $http, item, companies) {
    // items is injected in the controller, not its scope!
    $scope.items = item;
    $scope.companies = companies;

    $scope.cancel = function () {

        $mdDialog.cancel();
        
    };
    $scope.upadteProject = function () {

        $http({
            url: 'Project/UpdateProject',
            method: "GET",
            params: {
                newProject: $scope.items
            }
        }).success(function (updatedProject) {
            $scope.items.ExecutorCompany.Name = updatedProject.ExecutorCompany.Name;
            $scope.items.ClientCompany.Name = updatedProject.ClientCompany.Name;
            $mdDialog.cancel();
        });

    };
});