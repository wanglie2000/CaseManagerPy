from urllib import request, parse
import base64
import json
import os

class IdentifyingCodeDistinguish:
    def getFileFullPath(self,filePath):
        pwd = os.getcwd()
        tmp = pwd.split("src")[0]
        if (filePath[1:2] != ':'):
            # 是相对路径 要到files下面去找

            filesDir = os.path.abspath(tmp + os.path.sep + "..") + "\\files"
            picPath = filePath.replace("/", "\\")
            if (not filePath.startswith("\\")):
                filePath = "\\" + filePath
                filePath = filesDir + filePath

        return filePath

    def distinguish(self,picPath):
        picPath  = self.getFileFullPath(picPath)
        f = open(picPath, 'rb')
        # 参数images：图像base64编码
        img = base64.b64encode(f.read())
        # 解码转成字符串
        img_string = img.decode('utf-8')
        showapi_appid = "59367"
        showapi_sign = "30b53ec544ca4b2ab8e05b735d4eed0c"  # 替换此值
        url = "http://route.showapi.com/184-5"
        send_data = parse.urlencode([
            ('showapi_appid', showapi_appid)
            , ('showapi_sign', showapi_sign)
            , ('img_base64', img_string)
            , ('typeId', "30")
            , ('convert_to_jpg', "0")

        ])

        req = request.Request(url)
        try:
            response = request.urlopen(req, data=send_data.encode('utf-8'), timeout=10)  # 10秒超时反馈
        except Exception as e:
            print(e)
        result = response.read().decode('utf-8')
        result_json = json.loads(result)

        code = result_json['showapi_res_body']['Result']

        print(code)

        return code

