

#encoding=utf-8

from frame.PageOfPublic import PageOfPublic

class PageOfTest(PageOfPublic):

    def alertTest(self):
        if self.lastStepIsPass():
            try:
                self.click('id1','按钮')
                self.sleep(4)
                self.alertAccept()

                self.click('id1', '按钮')
                self.sleep(4)
                self.alertAccept()
                self.sleep(6)



            except Exception as e:
                self.setFailed(e)