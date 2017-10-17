myApp.factory("dataService", ["$http", "$location", function ($http, $location) {
    var promise = null;
    var host = $location.host();
    var port = $location.port();
    var protocol = $location.protocol();
    var service = {
        getStudents: function (page, records, search, sortKey, isAscSort) {
            promise = $http({
                url: protocol + "://" + host + ":" + port + "/api/Student/GetAllStudents?currentPage=" + page + "&recordsPerPage=" + records + "&search=" + search + "&sortKey=" + sortKey + "&isAscSort=" + isAscSort,
                method: "GET"
            }).then(function (response) {
                var studentInfo = response.data;
                return studentInfo;
            });
            return promise;
        }
    };
    return service;
}])