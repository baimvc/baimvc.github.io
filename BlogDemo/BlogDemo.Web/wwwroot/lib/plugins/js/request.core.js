/**
* 爱创科技
*/
/** @brief   The acctrue. */
var Acctrue = Acctrue || {};

/**
 * 爱创科技-网络工具.
 *
 * @author  First
 * @date    2019-01-15
 *
 * @returns .
 */
Acctrue.Request = function () {
    //内核版本
    var core_version = '2019-1-14 15:49:58';
    reqImg = function (/*请求地址*/url,/*对象的样式*/obj) {
        var xhr = new XMLHttpRequest();
        xhr.open('GET', url, true);
        xhr.responseType = "blob";
        xhr.setRequestHeader("client_type", "DESKTOP_WEB");
        //xhr.setRequestHeader("desktop_web_access_key", _desktop_web_access_key);
        xhr.onload = function () {
            if (this.status == 200) {
                var blob = this.response;
                var img = document.createElement("img");
                img.onload = function (e) {
                    window.URL.revokeObjectURL(img.src);
                };
                img.src = window.URL.createObjectURL(blob);
                img.style.width = obj.width;
                img.style.height = obj.height;
                document.getElementById(obj.name).appendChild(img);
            }
        }
        xhr.send();
        console.info("获取图片 %c 完成,程序内核：" + core_version, "color:green");
    }
    //Get
    createObjectGet = function (/*需要创建的对象*/obj) {
        var xhr = new XMLHttpRequest();
        if (obj.method === undefined) {
            obj.method = 'GET';
        }
        xhr.open(obj.method, obj.url, true);
        xhr.responseType = "blob";
        xhr.setRequestHeader("client_type", "DESKTOP_WEB");
        //xhr.setRequestHeader("desktop_web_access_key", _desktop_web_access_key);
        xhr.onload = function () {
            if (this.status === 200) {
                var blob = this.response;
                var item = document.createElement(obj.type);
                item.onload = function (e) {
                    window.URL.revokeObjectURL(item.src);
                };
                item.src = window.URL.createObjectURL(blob);
                item.style.width = obj.width;
                item.style.height = obj.height;
                document.getElementById(obj.id).appendChild(item);
            }
        }
        xhr.send();
        console.info("Get方式获取数据 %c 完成,程序内核：" + core_version, "color:green");
    }
    //POST
    createObjectPost = function (/*需要创建的对象*/obj) {
        var xhr = new XMLHttpRequest();
        if (obj.method === undefined) {
            obj.method = 'POST';
        }
        xhr.open(obj.method, obj.url, true);
        //post 请求头
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset-UTF-8')
        xhr.responseType = "blob";
        xhr.setRequestHeader("client_type", "DESKTOP_WEB");
        //xhr.setRequestHeader("desktop_web_access_key", _desktop_web_access_key);
        xhr.onload = function () {
            if (this.status === 200) {
                var blob = this.response;
                var item = document.createElement(obj.type);
                item.onload = function (e) {
                    window.URL.revokeObjectURL(item.src);
                };
                item.src = window.URL.createObjectURL(blob);
                item.style.width = obj.width;
                item.style.height = obj.height;
                if (obj.props === undefined) {
                    obj.props = {}
                }
                for (var k in obj.props) {
                    item.setAttribute(k, obj.props[k]);
                }
                document.getElementById(obj.id).appendChild(item);
            }
        }
        xhr.send(obj.para.data);
        console.info("Post方式获取数据 %c 完成,程序内核：" + core_version, "color:green");
    }

    return {
        /**
        * Get方式创建对象和传递参数
        */
        Get: createObjectGet,
        /**
        * Post方式创建对象和传递参数
        */
        Post: createObjectPost
    }
}();