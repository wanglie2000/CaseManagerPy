


from frame.ModulOfPublic import ModulOfPublic
from frame.tools.MySqlConnect import MySqlConnect
from frame.tools.DingSender import DingSender

import datetime



class WalmartQuery( ModulOfPublic ):
    ####查询order_attr并发送钉钉消息####
    def query(self):
        if self.lastStepIsPass():
            try:

                dbUrl = self.getPar('数据库地址')
                dbPort = self.getPar('数据库端口')
                dbName = self.getPar('数据库名')
                dbUser = self.getPar('数据库用户名')
                dbPassword = self.getPar('数据库密码')
                sql = self.getPar("SQL")



                print(dbUrl)
                print(dbPort)
                print(dbName)
                print(dbUser)
                print(dbPassword)

                print( int(dbPort) )


                dingSenderUser=self.getPar("钉钉发送用户名")
                dingSenderPassword = self.getPar("钉钉发送密码")

                dingReceivers = self.getPar("钉钉接收人").split(",")



                mySqlConnect = MySqlConnect(dbUrl, int(dbPort) ,dbName,dbUser,dbPassword ,self.logger )

                today = datetime.date.today()
                yesterday = today - datetime.timedelta(days=1)
                todayStr = today.strftime("%Y-%m-%d")
                yesterdayStr = yesterday.strftime("%Y-%m-%d")

                # sql="SELECT orderState,count(1) 数量 FROM `order_attr` WHERE createTime BETWEEN '"+ yesterdayStr +" 08:30:00' AND '"+ todayStr +" 08:30:00' GROUP BY orderState"



                text =  datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S" ) + "查询结果\r\n" +  mySqlConnect.query(sql)

                dingSender = DingSender( dingSenderUser,dingSenderPassword ,self.logger )

                dingSender.sendMsg(dingReceivers,text)


            except Exception as e:
                self.setFailed(e)
