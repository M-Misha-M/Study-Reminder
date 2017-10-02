myApp.factory("dataService", ["$http", "$q", function ($http, $q) {
    var promise = null;

    var service = {

        getStudents: function (page, records ,  search) {
            promise = $http({
                url: "https://localhost:44308/api/Student/GetAllStudents?currentPage=" + page + "&" + "recordsPerPage=" + records + "&" + "search=" + search,
                method: "GET"
            }).then(function(response) {
                var studentInfo = response.data;
                return studentInfo;
            });

            return promise;
        }


    };
    return service;
    //var _students = [];
 
    //var deferred = $q.defer();
 
    //var _getStudents = function (options) {
 
    //    $http.get("https://localhost:44308/api/Student/GetAllStudents?currentPage=" + options.currentPage + "&" + "recordsPerPage=" + options.recordsPerPage)
    //        .then(function (result) {
    //            angular.copy(result.data.Students, _students);
    //            deferred.resolve(result.data.RecordCount);
    //        },
    //        function () {
    //            deferred.reject();
    //        });
 
    //    return deferred.promise;
    //};
 
    //return {
    //    students:_students,
    //    getStudents: _getStudents,
    //};
}])