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
   
}])