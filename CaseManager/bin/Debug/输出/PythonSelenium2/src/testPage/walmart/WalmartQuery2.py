

from frame.PageOfPublic import PageOfPublic
from frame.ModulOfPublic import ModulOfPublic
from frame.tools.MySqlConnect import MySqlConnect
from frame.tools.DingSender import DingSender

import datetime
import time



class WalmartQuery2( PageOfPublic):
    ####eeee####
    def query(self):

        try:
            dingUser=self.getPar("钉钉登录用户")
            dingPassword = self.getPar("钉钉登录密码")

            dingReceivers  = self.getPar("钉钉接收人").split(",")

            h1 = int(self.getPar("小时数1"))
            h2 = int(self.getPar("小时数2"))

            now =  datetime.datetime.now()
            t1 = now -datetime.timedelta(hours=h1)
            t2 = now - datetime.timedelta(hours=h2)

            time1= t1.strftime("%Y-%m-%d %H:%M:%S" )
            time2 = t2.strftime("%Y-%m-%d %H:%M:%S")

            print( "检查时间从  " + time1  )
            print("检查时间到  " + time2 )



            url = self.driver.current_url + "&"+ "start='"+ time1 +"'&end='"+ time2 +"'"

            self.driver.get(url)
            self.sleep(1)

            text = self.getText("//body","整个页面")

            dingSender = DingSender(dingUser, dingPassword, self.logger)

            if "Created" in text or not("Acknowledged" in text):

                dingSender.sendMsg(dingReceivers, text)

            else:
                dingSender.sendMsg(dingReceivers,now.strftime("%Y-%m-%d %H:%M:%S" ) + "自动检查结果正常")








        except Exception as e:

            self.setFailed(e)