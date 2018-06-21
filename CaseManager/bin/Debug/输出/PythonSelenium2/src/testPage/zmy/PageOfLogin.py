#coding=utf-8

from frame.EleDescription import EleDescription
from frame.PageOfPublic import PageOfPublic
from frame.FrameSwitcher import FrameSwitcher



class PageOfLogin(PageOfPublic ):
    ####登录####
    def login(self):

        if self.lastStepIsPass():
            try:

                userName = self.getPar("用户名")
                passWord = self.getPar("密码")

                self.input("user","用户名输入框", userName)
                self.input("password","密码输入框", passWord)
                self.click("login","登录按钮")


            except Exception as e :
                self.setFailed(e)
