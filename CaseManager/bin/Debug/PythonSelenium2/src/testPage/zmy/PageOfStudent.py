#coding=utf-8
from frame.PageOfPublic import PageOfPublic

class PageOfStudent(PageOfPublic):

    def inputSubjects(self,subjects):
        id=""
        tmps = subjects.split(",")

        for tmp in tmps:
            if tmp=="少儿创意画":
                id="paint001"
            elif tmp=="线描写生":
                id = "paint002"
            elif tmp=="素描":
                id = "paint003"
            elif tmp=="硬笔书法":
                id = "calligraphy001"
            elif tmp=="毛笔书法":
                id = "calligraphy002"
            elif tmp=="中国舞":
                id = "dance001"
            elif tmp=="拉丁舞":
                id = "dance002"
            elif tmp=="街舞":
                id = "dance003"
            elif tmp=="跆拳道":
                id = "sport001"
            elif tmp=="午托":
                id = "care001"
            elif tmp=="晚托":
                 id="care002"
            self.click(id,tmp+"的复选框")



    ####新建学生####
    def createStudent(self):
        if self.lastStepIsPass():
            try:

                #得到参数
                name = self.getPar('姓名')
                nick = self.getPar('昵称')
                sex = self.getPar('性别')
                birthday = self.getPar("生日")
                guardian = self.getPar('家长')
                subjects = self.getPar('科目')
                mobile = self.getPar('手机')
                rank = self.getPar("客户等级")




                self.click('create', '新建按钮')

                if(None!=name):
                    self.input('fullname','姓名输入框',name)

                if( None!=nick ):
                    self.input("nickname","昵称输入框",nick)

                if(None!=sex):
                    if( "男"==sex ):
                        xpath="//input[@name='sex' and @value='1']"
                    else:
                        xpath = "//input[@name='sex' and @value='0']"
                    self.click(xpath,"性别选择"+sex)
                if( None!= birthday):
                    self.editAttr("birthday","生日输入框","type","text")
                    self.input("birthday","生日输入框",birthday)
                if( None!=guardian ):
                    self.input("guardian","家长输入框",guardian)

                if( None!= subjects):
                    self.inputSubjects(subjects)

                if(None!=mobile):
                    self.input("mobile","手机输入框",mobile)

                if(None!=rank):
                    self.selectByText("rank","客户等级",rank)

                self.sleep(1)

                self.click("save","保存按钮")
                self.sleep(3)
                self.alertAccept()

                self.sleep(1)
                self.back()
                self.sleep(3)

                self.click("end","最后一页按钮")

                self.sleep(4)


                xx= self.getText(  "//div[@id='result']//tbody/tr[last()]/td[@field='fullname']","最后一行的姓名")
                print(xx)
                if(    name!= xx):
                    raise Exception("没有在最后一页的最后一行找到刚刚输入的学生")



            except Exception as e :
                self.setFailed(e)
