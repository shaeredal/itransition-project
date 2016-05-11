var app = angular.module('ComixAdd', []);
app.controller('switchTemplateController', function ($scope) {
    $scope.items = ['skew', 'triad', 'tetrad'];
    //$scope.$watch('selection', function () { make_it_draggable(); })
    $scope.selection = $scope.items[0];
    $scope.remove = function ($event) {
        angular.element($event.target).parent().parent().remove();
    }
});

app.directive('fileDropzone', function () {
        return {
            restrict: 'A',
            scope: {
                file: '=',
                fileName: '='
            },
            link: function (scope, element, attrs) {

                var checkSize, isTypeValid, processDragOverOrEnter, validMimeTypes;

                processDragOverOrEnter = function (event) {
                    if (event != null) {
                        event.preventDefault();
                    }
                    event = event.originalEvent;
                    event.dataTransfer.effectAllowed = 'copy';
                    return false;
                };
                validMimeTypes = attrs.fileDropzone;
                checkSize = function (size) {
                    var _ref;
                    if (((_ref = attrs.maxFileSize) === (void 0) || _ref === '') || (size / 1024) / 1024 < attrs.maxFileSize) {
                        return true;
                    } else {
                        alert("File must be smaller than " + attrs.maxFileSize + " MB");
                        return false;
                    }
                };
                isTypeValid = function (type) {
                    if ((validMimeTypes === (void 0) || validMimeTypes === '') || validMimeTypes.indexOf(type) > -1) {
                        return true;
                    } else {
                        alert("Invalid file type.  File must be one of following types " + validMimeTypes);
                        return false;
                    }
                };
                element.bind('dragover', processDragOverOrEnter);
                element.bind('dragenter', processDragOverOrEnter);
                return element.bind('drop', function (event) {
                    var file, name, reader, size, type;
                    if (event != null) {
                        event.preventDefault();
                    }
                    event = event.originalEvent;
                    reader = new FileReader();
                    reader.onload = function (evt) {
                        if (checkSize(size) && isTypeValid(type)) {
                            return scope.$apply(function () {
                                scope.file = evt.target.result;
                                if (angular.isString(scope.fileName)) {
                                    return scope.fileName = name;
                                }
                            });
                        }
                    };
                    try{
                        file = event.dataTransfer.files[0];

                        name = file.name;
                        type = file.type;
                        size = file.size;
                        reader.readAsDataURL(file);
                    }
                    catch (e) {
                        return false;
                    }
                    return false;
                });
            }
        };
    });

app.controller('dragndrop', function ($scope) {
    $scope.image = null;
    $scope.imageFileName = '';
    $scope.remove = function ($event) {
        angular.element($event.target).parent().remove();
    }
});



app.controller("pagesController", function ($scope) {
    $scope.page = '/Comix/ComixPage';
    $scope.drgbl = function () {
        $(".frame-image").draggable({
            //containment: "parent"
        }).resizable();
        $('.frame-image').bind('mousewheel', function (e) {
            if (e.originalEvent.deltaY < 0) {
                $(this).css("width", "+=16");
                $(this).css("height", "+=16");
            }
            else {
                $(this).css("width", "-=16");
                $(this).css("height", "-=16");
            }
            return false;
        });

        $("#balloons_panel .talkbubble").draggable(
            { helper: "clone" }).resizable({ containment: "parent" });

        $(".frame-image").droppable({
            accept: "#balloons_panel .talkbubble",
            drop: function (event, ui) {
                var clone = ui.draggable.clone();
                clone.draggable({
                    containment: "parent"
                }).resizable({ containment: "parent" });
                
                clone.css("top", ui.offset.top - $(this).offset().top);
                clone.css("left", ui.offset.left - $(this).offset().left);
                clone.css("position", "absolute");

                clone.find('.delete-cross').click(function () { clone.remove(); });

                $(this).append(clone);
            }
        });
    };
    $scope.$watch(function () {
        return angular.element;
    }, function () {
            $scope.$evalAsync(function () {
                $scope.drgbl();
            });
        });
    }).directive("comixManager", function ($compile) {
    return {
        templateUrl: '/Comix/ComixPage',
        restrict: 'E',
        link: function (scope, elm) {
            scope.add = function () {
                angular.element("#pages").append($compile('<comix-manager></comix-manager>')(scope));
                //scope.drgbl();
            }
        }
    };
    });



//app.directive('drgbl', function($timeout) {
//    return {
//        link: function(scope, element, attr) {
//            $timeout(function() {
//                make_it_draggable();
//                });
//        }
//    }
//});