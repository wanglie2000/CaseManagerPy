import smtplib
from email.mime.text import MIMEText
from email.header import Header

import json



#
# smtpObj=smtplib.SMTP("SMTP.163.com")
#
# smtpObj.login("wanglie2000","Wl@780107")
#
# message = MIMEText('Python 邮件发送测试...')
# message['Subject']=Header("标题")
#
# print(  message.as_string())
#
# smtpObj.sendmail("wanglie2000@163.com",["wanglie2018@163.com"],message.as_string() )


text = '{"errno":0,"msg":"","result":{"tool":32300,"adapter":32101}}';
jo =  json.loads(text)

print(jo["result"]["tool"])

