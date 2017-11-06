var myApp = angular.module('studentApp.simpleGrid', ['ui.bootstrap'])

myApp.controller("studentController", ["$scope", "dataService",
    function ($scope, dataService) {
        $scope.totalItems = 0;
        $scope.currentPage = 1;
        $scope.maxSize = 5;
        $scope.recordsPerPage = 5;
        $scope.NumberOfPageButtons = 5;
        $scope.search = "";
        // column to sort
        $scope.sortKey = 'Name';
        $scope.isAscSort = true;
        $scope.typeOptions = [
            { name: 5, value: 5 },
            { name: 8, value: 8 },
            { name: 10, value: 10 }
        ];
        const arrowUp = 'arrow-up';
        const arrowDown = 'arrow-down';
        const arrowAll = 'arrow-all';

        $scope.getData = function () {
            dataService.getStudents($scope.currentPage, $scope.recordsPerPage, $scope.search, $scope.sortKey, $scope.isAscSort)
             .then(function (studentInfo) {
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
            if ($scope.sortKey == col)
            {
                if ($scope.isAscSort)
                {
                  return arrowUp;
                }
                else
                {
                  return arrowDown;
                }
            }
            else
            {
              return arrowAll;
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



