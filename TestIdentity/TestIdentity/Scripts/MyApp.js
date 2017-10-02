var App = angular.module('MyApp', ['ngRoute']); 
App.controller('StudentDataController', function ($scope, AddDataService) {
    //veriables with default values.
    $scope.submitText = "Save";
    $scope.submitted = false;
    $scope.message = '';
    $scope.isFormValid = false;
    $scope.Student = {
        Id: '',
        Name: '',
        LastName: '',
        Age:'',
        Email: '',
        Address: ''
    };
    $scope.$watch('form1.$valid', function (newValue) { // form1 is our form name
        $scope.isFormValid = newValue;
    });

    $scope.SaveData = function (data) {
        if ($scope.submitText == 'Save') {
            $scope.submitted = true;
            $scope.message = '';

            if ($scope.isFormValid) {
                $scope.submitText = 'Please Wait...';
                $scope.User = data;
                RegistrationService.SaveFormData($scope.User).then(function (d) {
                    alert(d);
                    if (d == 'Success') {
                        //have to clear form here
                        ClearForm();
                    }
                    $scope.submitText = "Save";
                });
            }
            else {
                $scope.message = '';
            }
        }
    }


    function ClearForm() {
        $scope.User = {};
        $scope.f1.$setPristine(); //here f1 is form name
        $scope.submitted = false;
    }
}).factory('RegistrationService', function ($http, $q) { 
    
    var fac = {};
    fac.SaveFormData = function (data) {
        var defer = $q.defer();
        $http({
            url: '/Home/Register',
            method: 'POST',
            data: JSON.stringify(data),
            headers: {'content-type' : 'application/json'}
        }).success(function (d) {
            // Success callback
            defer.resolve(d);
        }).error(function (e) {
            //Failed Callback
            alert('Error!');
            defer.reject(e);
        });
        return defer.promise;
    }
    return fac;
});
