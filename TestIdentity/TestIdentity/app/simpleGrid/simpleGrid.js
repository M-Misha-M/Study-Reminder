var myApp = angular.module('ngWebApiGrid.simpleGrid', ['ui.bootstrap'])



myApp.controller("entityController", ["$scope", "dataService","localStorageService",
    function ($scope, dataService, localStorageService) {
      

        $scope.totalItems = 0;
        $scope.currentPage = 1;
        $scope.maxSize = 5;
        $scope.recordsPerPage = 5;
        $scope.NumberOfPageButtons = 5;
        $scope.search = "";
        $scope.pagingInfo = {         
            sortBy: 'Name',          
          search: ''        
        };

        $scope.getData = function () {
            dataService.getStudents($scope.currentPage, $scope.recordsPerPage , $scope.search).then(function (studentInfo) {
                $scope.totalItems = studentInfo.RecordCount;
                $scope.data = studentInfo.Students;
                


            }, function (response) {
                alert("error occurred: unable to get data");
            });
        }
       

      

        $scope.pageChanged = function () {
            $scope.getData();
        };

        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.reverse = !$scope.reverse; //if true make it false and vice versa
        }

        $scope.searchFor = function () {
            $scope.getData();
        }
        
        $scope.getData();
        

    }]);



