#encoding=utf-8

import requests
import json

class DingSender(object):
    dingUser=None
    dingPassword=None
    logger = None
    def __init__(self ,dingUser,dingPassword ,logger=None):
        self.dingUser=dingUser
        self.dingPassword=dingPassword
        self.logger=logger

    def sendMsg(self, userList , text):
        headers = {'Content-Type': 'application/json'}
        postData = { "username": self.dingUser ,"password":  self.dingPassword}
        response = requests.post("https://userinfo.youkeshu.com/api/get_token", postData, headers)
        token = response.json().get("token")
        postData = {"msg": text ,"user": userList }
        print( postData )
        print("https://userinfo.youkeshu.com/api/send_ding_msg?token=" + token)
        print( json.dumps(postData) )

        response = requests.post("https://userinfo.youkeshu.com/api/send_ding_msg?token=" + token, data=json.dumps(postData)   ,  headers=headers)
        if None!=self.logger:
            self.logger.appendContent("发送钉钉消息接收人=" +  str(userList))
            self.logger.appendContent("发送钉钉消息内容=" + text)
        print(response.text)

if __name__=="__main__":
    DingSender("wanglie","Wl@780107").sendMsg(["wanglie"],"abcde")










