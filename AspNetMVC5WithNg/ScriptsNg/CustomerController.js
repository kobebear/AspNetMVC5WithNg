angular.module('myFormApp', [])
    .controller('CustomerController', function ($scope, $http, $location, $window) {
        $scope.custModel = {};
        $scope.message = '';
        $scope.result = "color-default";
        $scope.isViewLoading = false;
        $scope.ListCustomer = null;
        getallData();

        //******=========Get All Customer=========******
        function getallData() {
            //debugger;
            $http.get('/Home/GetAllData')
                //新版ng支持promise，将success和error回调函数，替换为then和catch
                .then(function (data) {//且，请求只获得一个对象data。
                    console.log(data); //状态等其他信息，都包含在data中
                    $scope.ListCustomer = data.data; //实际响应结果数据，保存在data.data中
                })
                .catch(function (err) {
                    $scope.message = 'Unexpected Error while loading data!!';
                    $scope.result = "color-red";
                    console.log($scope.message);
                });
        };

        //******=========Get Single Customer=========******
        $scope.getCustomer = function (custModel) {
            $http.get('/Home/GetbyID/' + custModel.Id)
                .then(function (data) {
                    //debugger;
                    console.log(data);
                    $scope.custModel = data.data;
                    getallData();
                })
                .catch(function (err) {
                    $scope.message = 'Unexpected Error while loading data!!';
                    $scope.result = "color-red";
                    console.log($scope.message);
                });
        };

        //******=========Save Customer=========******
        $scope.saveCustomer = function () {
            $scope.isViewLoading = true;

            $http({
                method: 'POST',
                url: '/Home/Insert',
                data: $scope.custModel
            }).then(function (data) {
                console.log(data);
                if (data.statusText === "OK") { //返回结果的状态信息，以文字方式，保存在data.statusText中
                    $scope.message = 'Form data Saved!';
                    $scope.result = "color-green";
                    getallData();
                    $scope.custModel = {};
                }
                else {
                    $scope.message = 'Form data not Saved!';
                    $scope.result = "color-red";
                }
            }).catch(function (err) {
                console.log(err);
                $scope.message = 'Unexpected Error while saving data!!' + err.errors;
                $scope.result = "color-red";
                console.log($scope.message);
            });
            getallData();
            $scope.isViewLoading = false;
        };

        //******=========Edit Customer=========******
        $scope.updateCustomer = function () {
            //debugger;
            $scope.isViewLoading = true;
            $http({
                method: 'POST',
                url: '/Home/Update',
                data: $scope.custModel
            }).then(function (data) {
                console.log(data);
                if (data.statusText === "OK") {
                    $scope.custModel = null;
                    $scope.message = 'Form data Updated!';
                    $scope.result = "color-green";
                    getallData();
                }
                else {
                    $scope.message = 'Form data not Updated!';
                    $scope.result = "color-red";
                }
            }).catch(function (err) {
                console.log(err);
                $scope.message = 'Unexpected Error while updating data!!' + err.errors;
                $scope.result = "color-red";
                console.log($scope.message);
            });
            $scope.isViewLoading = false;
        };

        //******=========Delete Customer=========******
        $scope.deleteCustomer = function (custModel) {
            //debugger;
            var IsConf = confirm('You are about to delete ' + custModel.CustName + '. Are you sure?');
            if (IsConf) {
                $http.post('/Home/Delete/' + custModel.Id)
                    .then(function (data) {
                        console.log(data);
                        if (data.statusText === "OK") {
                            $scope.message = custModel.CustName + ' deleted from record!!';
                            $scope.result = "color-green";
                            getallData();
                        }
                        else {
                            $scope.message = 'Error on deleting Record!';
                            $scope.result = "color-red";
                        }
                    })
                    .catch(function (err) {
                        console.log(err);
                        $scope.message = 'Unexpected Error while deleting data!!';
                        $scope.result = "color-red";
                        console.log($scope.message);
                    });
            }
        };
    })
    .config(function ($locationProvider) {
        $locationProvider.html5Mode(true);
    });