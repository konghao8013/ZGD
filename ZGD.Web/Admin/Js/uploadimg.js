var imgMethod = {
    //id 
    //btn 上传按钮Id
    //input hide的图片地址
    //type 图片所属类型  editor,uploadrepeatimages,uploadhospital,hospitalreg,product,cuthospital,activity
    //delete 删除
    //imgnum 图片数量 默认1单图
    //back 图片
    init: function (id, btn, input, type, imgnum, deleteimg, imgServerUrl, back) {
        var $ = jQuery,
            $list = $('#' + id),
            // 优化retina, 在retina下这个值是2
            ratio_zz = window.devicePixelRatio || 1,

            // 缩略图大小
            thumbnailWidth_zz = 100 * ratio_zz,
            thumbnailHeight_zz = 100 * ratio_zz,

            // Web Uploader实例
            uploader;

        // 初始化Web Uploader
        uploader = WebUploader.create({
            // 自动上传。
            auto: true,
            // swf文件路径
            swf: 'https://cdn.staticfile.org/webuploader/0.1.0/Uploader.swf',
            // 文件接收服务端。
            server: '/tools/upload_ajax.aspx?action=' + type,
            // 选择文件的按钮。可选。
            // 内部根据当前运行是创建，可能是input元素，也可能是flash.
            pick: '#' + btn,
            //
            resize: false, //尺寸不改变
            threads: 1, // [默认值：3] 上传并发数。允许同时最大上传进程数。
            compress: false,//不启用压缩
            //// 配置压缩的图片的选项。如果此选项为false, 则图片在上传前不进行压缩。  
            //compress: {
            //    width: 1024,
            //    height: 1024,
            //    // 图片质量，只有type为`image/jpeg`的时候才有效。  
            //    quality: 90,
            //    // 是否允许放大，如果想要生成小图的时候不失真，此选项应该设置为false.  
            //    allowMagnify: false,
            //    // 是否允许裁剪。  
            //    crop: false,
            //    // 是否保留头部meta信息。  
            //    preserveHeaders: true,
            //    // 如果发现压缩后文件大小比原来还大，则使用原来图片  
            //    // 此属性可能会影响图片自动纠正功能  
            //    noCompressIfLarger: false,
            //    // 单位字节，如果图片大小小于此值，不会采用压缩。  
            //    compressSize: 0
            //},
            fileSingleSizeLimit: 1024 * 1024 * 4, //  单个文件大小
            // 只允许选择文件，可选。
            accept: {
                title: 'Images',
                extensions: 'gif,jpg,jpeg,bmp,png',
                mimeTypes: 'image/jpg,image/jpeg,image/png'
            }
        });
        uploader.on('error', function (handler, max) {
            if (handler == "Q_EXCEED_NUM_LIMIT") {
                layer.msg("最多只能上传 " + max + "张图片");
            }
            if (handler == "F_EXCEED_SIZE") {
                layer.msg("超过文件大小限制" + max / 1024 / 1024 + "M");
            }
        });
        uploader.on("uploadAccept", function (file, response) {
            if (response.success == "0") {
                $list.find("img").removeAttr("src");
                parent.layer.msg(response.msg, { icon: 5, time: 1500 });
            }
        });
        // 当有文件添加进来的时候
        uploader.on('fileQueued', function (file) {
            if ($list.children(".file-box").length >= imgnum && imgnum != 1) { //.children("div .file-item")
                layer.alert("最多只能添加" + imgnum + "张图片！");
                return;
            }

            var $li = back(file, { url: "" }),
                $img = $li.find('img');
            if (imgnum > 1) {
                $list.append($li);
            }
            else {
                $list.html($li);
            }

            // 创建缩略图
            uploader.makeThumb(file, function (error, src) {
                if (error) {
                    $img.replaceWith('<span>不能预览</span>');
                    return;
                }
                $img.attr('src', src);
            }, thumbnailWidth_zz, thumbnailHeight_zz);
        });

        // 文件上传过程中创建进度条实时显示。
        uploader.on('uploadProgress', function (file, percentage) {
            var $li = $('#' + file.id),
                $percent = $li.find('.progress span');

            // 避免重复创建
            if (!$percent.length) {
                $percent = $('<p class="progress img_up"><span>上传中...</span></p>')
                    .appendTo($li)
                    .find('span');
            }
            $percent.css('width', percentage * 100 + '%');
        });

        // 文件上传成功，给item添加成功class, 用样式标记上传成功。
        uploader.on('uploadSuccess', function (file, response) {
            $('#' + file.id).addClass('upload-state-done');
            var imgCount = $('#' + id).children("div .file-item").length;
            if (response != null && response.status == 1) {

                $('#' + file.id + " img").attr('src', imgServerUrl + response.path);
                $('#' + file.id + " img").attr('data-src', response.path);

                //删除
                $("#delimg_" + deleteimg + file.id).unbind('click');
                $("#delimg_" + deleteimg + file.id).bind('click', function () {
                    imgMethod.dealImg(input, deleteimg, file.id, uploader);
                });

                if (imgnum > 1) {
                    var imgs = $("#" + input).val();
                    if ($.trim(imgs) == "") {
                        $("#" + input).val(response.path);
                    }
                    else if (imgs.split(',').length < imgnum) {
                        $("#" + input).val(imgs + "," + response.path);
                    }
                } else {
                    $("#" + input).val(response.path);
                }
            }
        });

        // 文件上传失败，现实上传出错。
        uploader.on('uploadError', function (file) {
            var $li = $('#' + file.id),
                $error = $li.find('div.error');

            // 避免重复创建
            if (!$error.length) {
                $error = $('<div class="progress img_up">上传失败</div>').appendTo($li);
            }
        });

        // 完成上传完了，成功或者失败，先删除进度条。
        uploader.on('uploadComplete', function (file) {
            $('#' + file.id).find('.progress').remove();
            $('.progress').remove();
        });
    },
    dealImg: function (input, type, fileId, uploader) {
        var obj = $("#" + input);
        var imgs = obj.val();
        if ($.trim(imgs) != "" && imgs != undefined) {
            var img_list = imgs.split(',');
            var del_img = $("#img_" + type + fileId);
            $.each(img_list, function (i, m) {
                if (m == del_img.attr("data-src")) {
                    $("#file_" + type + fileId).remove();
                    img_list.splice(i, 1);
                    if (uploader) {
                        uploader.removeFile(fileId, true);
                    }
                    $("#" + input).val(img_list.join(','));
                    return;
                }
            });
        }
    }
}