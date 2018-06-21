import smtplib
from email.mime.text import MIMEText
from email.header import Header
from email.mime.image import MIMEImage
from email.mime.multipart import MIMEMultipart

from email.mime.application import MIMEApplication

class MailSender(object):
    def send(self, smtp,sender,password , receivers, subject ,content ,imgPath=None ):


        smtpObj = smtplib.SMTP(smtp)
        smtpObj.login(sender, password)
        message = MIMEMultipart('related')
        message['Subject'] = Header(subject)


        # jpgpart = MIMEApplication(open('d:/a.jpg', 'rb').read())
        # jpgpart.add_header('Content-Disposition', 'attachment', filename='a.jpg')
        # message.attach(jpgpart)

        if imgPath!=None:
            content = content + '<br><img alt="" src="cid:image1" />'
            fp = open(imgPath, 'rb')
            msgImage = MIMEImage(fp.read())
            fp.close()

            msgImage.add_header('Content-ID', 'image1')
            message.attach(msgImage)


        msgText = MIMEText(content,"html","utf-8")
        message.attach(msgText)





        print(message.as_string())
        smtpObj.sendmail(sender,  receivers , message.as_string())
        smtpObj.quit()

if __name__=="__main__":
    MailSender().send("SMTP.163.com","wanglie2000@163.com","Wl@780107",["wanglie2018@163.com"],"标题","内容" )


