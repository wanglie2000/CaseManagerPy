# encoding=utf-8

import time

class ModulOfPublic(object):
    pars = None
    driver=None
    logger = None

    def __init__(self, pars, driver, logger):
        self.pars = pars
        self.logger = logger

    def lastStepIsPass(self):
        return self.pars.get("LASTSTEP")

    def setFailed(self, e):
        print('异常!!!!!!!!    ' + str(e))
        self.pars.put("LASTSTEP", False)

    # 休眠
    def sleep(self, second):
        time.sleep(second)

    # 获取参数
    def getPar(self, parName):
        return self.pars.get(parName)