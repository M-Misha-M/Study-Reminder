app.service('CRUDService', function ($http) {
    //**********----Get Record----***************
    this.getStudents = function (apiRoute) {
        return $http.get(apiRoute);
    }
});