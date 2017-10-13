var myApp = angular.module('ngWebApiGrid.simpleGrid', ['ui.bootstrap'])

myApp.controller("studentController", ["$scope", "dataService","localStorageService",
    function ($scope, dataService, localStorageService) {
        $scope.totalItems = 0;
        $scope.currentPage = 1;
        $scope.maxSize = 5;
        $scope.recordsPerPage = 5;
        $scope.NumberOfPageButtons = 5;
        $scope.search = "";

        // column to sort
        $scope.sortKey = 'Name';
        $scope.isAscSort = true;

        $scope.typeOptions =
       [
        { name: 5, value: 5 },
        { name: 8, value: 8 },
        { name: 10, value: 10 }
       ];

        $scope.getData = function () {
            dataService.getStudents($scope.currentPage, $scope.recordsPerPage, $scope.search, $scope.sortKey, $scope.isAscSort).then(function (studentInfo) {
                $scope.totalItems = studentInfo.RecordCount;
                $scope.data = studentInfo.Students;
                


            }, function (response) {
                alert("error occurred: unable to get data");
            });
        }
       

        $scope.pageChanged = function () {
            $scope.getData();
        };

        $scope.sort = function (col) {                    
            $scope.sortKey = col;
            $scope.isAscSort = !$scope.isAscSort;
            
            $scope.getData();
        };

   
        $scope.sortClass = function (col) {
            if ($scope.sortKey == col) {
                if ($scope.isAscSort) {
                    return 'arrow-up';
                } else {
                    return 'arrow-down';
                }
            } else {
                return 'arrow-all';
            }
        };


        $scope.searchFor = function () {
            $scope.getData();
        }

        $scope.changePageSize = function () {
            $scope.currentPage = 1;
            $scope.getData();
        };
        
        $scope.getData();        
    }]);



