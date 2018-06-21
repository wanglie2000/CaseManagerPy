# coding=utf-8

import time
import os

from frame.EleDescription import EleDescription
from frame.FrameSwitcher import FrameSwitcher
from frame.PageOfPublic import PageOfPublic
from frame.WindowSwitcher import WindowSwitcher
from frame.tools.UpLoadWindow import UpLoadWindow


class UpLoadTest(PageOfPublic):

    def upLoad(self):
        if self.lastStepIsPass():
            try:
                filePath = self.getPar("文件路径")
                self.click("id3", "上传文件的按钮")
                self.sleep(2)
                self.upload(filePath)


            except Exception as e:
                self.setFailed(e)


