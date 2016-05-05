var app = angular.module('ComixAdd', []);
app.controller('switchTemplateController', function ($scope) {
    $scope.items = ['skew', 'triad', 'tetrad'];
    $scope.selection = $scope.items[0];
});