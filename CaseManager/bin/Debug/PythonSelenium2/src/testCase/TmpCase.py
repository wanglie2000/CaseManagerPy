#encoding=utf-8

from frame.MyTestCase import MyTestCase
from frame.DriverInit import DriverInit
from frame.PageOfPublic import PageOfPublic
import time
import json
from testPage.demo.PageOfTest import PageOfTest
from testPage.walmart.WalmartQuery import WalmartQuery
from testPage.walmart.WalmartQuery2 import WalmartQuery2
from testPage.zmy.PageOfLogin import PageOfLogin
from testPage.zmy.PageOfStudent import PageOfStudent
from testPage.PageOfBaidu import PageOfBaidu
from testPage.SwitchWindowTest import SwitchWindowTest
from testPage.UpLoadTest import UpLoadTest






class TmpCase(MyTestCase):

    def test(self):
        print("test-----------------")
        pP = PageOfPublic(self.pars, None, self.logger)

        
        #步骤1生成的代码

        if self.getPar('LASTSTEP'):
            self.driver = DriverInit(self.logger).getWebDriver('firefox')
            pP=PageOfPublic(self.pars,self.driver,self.logger)

        #步骤2生成的代码

        if self.getPar('LASTSTEP'):
            self.openURL('http://www.autosofttest.com/zmy/login.jsp')



        #步骤5生成的代码

        self.setPar("用户名", "ylx")

        #步骤6生成的代码

        self.setPar("密码", "123456")

        #步骤7生成的代码

        PageOfLogin(self.pars, self.driver, self.logger).login()

        #步骤8生成的代码

        self.setPar("姓名", "abcde")

        #步骤9生成的代码

        PageOfStudent(self.pars, self.driver, self.logger).createStudent()

        #步骤10生成的代码



        #步骤11生成的代码



        #步骤12生成的代码

        time.sleep(3)

        #步骤13生成的代码

        if None!=self.driver:
            self.driver.quit()
            self.logger.appendContent("退出")


        
        
    def setUp(self):
        self.testClassName = __name__
        #print("类名=" + __name__)
        MyTestCase.setUp(self)