#encoding=utf-8
from frame.EleDescription import EleDescription
from frame.tools.UpLoadWindow import UpLoadWindow
from frame.FrameSwitcher import FrameSwitcher
from frame.WindowSwitcher import WindowSwitcher
from frame.tools.DingSender import DingSender
from frame.tools.MySqlConnect import MySqlConnect
from frame.tools.KeyboardAndMouse import KeyboardAndMouse
from frame.tools.IdentifyingCodeDistinguish import IdentifyingCodeDistinguish
from frame.tools.ExcelOperator import ExcelOperator
import time
import datetime
import os
import autoit
import random

class PageOfPublic(object):
    pars=None
    driver=None
    logger=None




    def __init__(self, pars,driver,logger):
        self.pars=pars
        self.driver= driver
        self.logger = logger

    def replacePars(self,text):
        tmp = text
        keys = self.pars.map.keys()
        for key in keys:
            if key!="LASTSTEP":
                tmp=tmp.replace("$"+ key +"$", str(self.getPar(key))  )
        return tmp

    def lastStepIsPass(self):
        return self.pars.get("LASTSTEP")

    def executeScript(self,jsStr):
        self.driver.execute_script(jsStr)
        
    def setFailed(self,e) :
        print('异常!!!!!!!!    '+str(e))
        self.pars.put("LASTSTEP", False);
        self.logger.snap()


    #单步方法
    def oneStep(self,actionName,actionPars):
        if self.lastStepIsPass():
            try:

                actionPars = self.replacePars(actionPars)
                pars = actionPars.split("||")

                if "点击" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    self.click(idOrXpath, eleName)

                elif "点击(硬)" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    self.click2(idOrXpath, eleName)

                elif "存储控件的图像"==actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    imgPath=pars[2]
                    self.saveElementImg(idOrXpath,eleName,imgPath)



                elif "点击windows控件"== actionName:
                    windowTitle = pars[0]
                    controlSysName= pars[1]
                    controlName = pars[1]
                    self.clickWindowsControl(windowTitle,controlSysName,controlName)

                elif "滚动到控件" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    self.scrollIntoView(idOrXpath,eleName)

                elif "选择单选按钮或复选框"== actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    checked = pars[2]
                    self.selectRadioOrCheckbox(idOrXpath,eleName,checked=="是")


                elif "悬停"==actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    self.moveTo(idOrXpath, eleName)


                elif "双击" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    self.doubleClick(idOrXpath,eleName)

                elif "右键" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    self.rightClick(idOrXpath,eleName)

                elif "拖拽" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    idOrXpath2 = pars[2]
                    eleName2 = pars[3]
                    self.drag(idOrXpath,eleName,idOrXpath2,eleName2)


                elif "输入" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    text = pars[2]
                    self.input(idOrXpath, eleName, text)

                elif "选择by文本" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    text = pars[2]
                    self.selectByText(idOrXpath, eleName, text)
                elif "选择by值" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    value = pars[2]
                    self.selectByValue  (idOrXpath, eleName, value)
                elif "选择by序号" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    index = pars[2]
                    self.selectByIndex(idOrXpath, eleName, index)
                elif "验证是否存在" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    exist = pars[2]
                    timeout= int(pars[3])
                    if exist=="是":
                        self.exist( idOrXpath,eleName,timeout )
                    else:
                        self.unExist(idOrXpath,eleName,timeout)

                elif "得到是否存在"== actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    parName = pars[2]

                    isExist = self.isExist(idOrXpath,eleName)
                    self.setPar(parName,isExist)


                elif "得到文本" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    parName = pars[2]

                    tmp =  self.getText(idOrXpath,eleName)
                    self.setPar( parName,tmp)

                elif "验证文本" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    expectText = pars[2]
                    tmp = self.getText(idOrXpath, eleName)
                    if tmp!=expectText:
                        raise Exception("控件[" + eleName + "]的实际文本是[" + tmp+"]，与期望文本["+ expectText + "]不符合")

                elif "得到值" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    parName = pars[2]
                    tmp = self.getValue(idOrXpath, eleName)
                    self.setPar(parName, tmp)


                elif "验证值" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    expectValue = pars[2]
                    tmp = self.getValue(idOrXpath, eleName)
                    if tmp != expectValue:
                        raise Exception("控件[" + eleName + "]的实际值是[" + tmp + "]，与期望值[" + expectValue + "]不符合")

                elif "验证是否选中" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    expectStat = pars[2]
                    checked = self.isChecked(idOrXpath,eleName)
                    if "是"==expectStat:
                        if not checked:
                            raise Exception("控件[" + eleName + "]的实际选中状态是[" + str(checked) + "]，与期望值[" + expectStat + "]不符合")
                    else:
                        if checked:
                            raise Exception("控件[" + eleName + "]的实际选中状态是[" + str(checked) + "]，与期望值[" + expectStat + "]不符合")
                elif "得到选中状态"==actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    parName = pars[2]
                    checked = self.isChecked(idOrXpath, eleName)
                    self.setPar(parName,checked)


                elif "得到属性" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    attrName = pars[2]
                    parName = pars[3]
                    tmp = self.getAttr(idOrXpath, eleName ,attrName)
                    self.setPar(parName, tmp)

                elif "验证属性" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    attrName = pars[2]
                    expectAttrValue = pars[3]
                    tmp = self.getAttr(idOrXpath, eleName, attrName)
                    if tmp != expectAttrValue:
                        raise Exception("控件[" + eleName + "]的["+ attrName +"]属性实际值是[" + tmp + "]，与期望值[" + expectAttrValue + "]不符合")

                elif "移除属性" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    attrName = pars[2]
                    self.removeAttr(idOrXpath,eleName,attrName)

                elif "修改属性" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    attrName = pars[2]
                    newAttrValue = pars[3]
                    self.editAttr(idOrXpath,eleName,attrName,newAttrValue)

                elif "强制可输入" == actionName:
                    idOrXpath = pars[0]
                    eleName = pars[1]
                    self.removeAttr(idOrXpath, eleName, "readonly")

                elif "进入iframe" == actionName:
                    idOrXpath = pars[0]
                    iframeName =pars[1]
                    self.switchFrame(idOrXpath,iframeName)

                elif "退出iframe" == actionName:
                    self.exitFrame()

                elif "切换窗口" == actionName:
                    index = int(pars[0])
                    self.switchWindow(index)

                elif "只保留第一窗口"== actionName:
                    self.closeAllwindowsExceptFirst()

                elif "回退窗口" == actionName:
                    self.back()

                elif "前进窗口" == actionName:
                    self.forword()

                elif "刷新窗口" == actionName:
                    self.refresh()

                elif "执行js" == actionName:
                    if len(pars)==1:
                        jsStr = pars[0]
                        self.exeJS(jsStr)
                    else:
                        jsStr = pars[0]
                        idOrXpath = pars[1]
                        eleName = pars[2]
                        self.exeJS(jsStr,idOrXpath,eleName)

                elif "发送钉钉消息"==actionName:
                    dingUser=  pars[0]
                    dingPassword = pars[1]
                    userList = pars[2].split(",")
                    content=  pars[3]
                    content=self.replacePars(content)
                    DingSender(dingUser,dingPassword,self.logger).sendMsg(userList,content)

                elif "得到相对日期"==actionName:
                    if len(pars)==2:
                        day = int(pars[0])
                        parName = pars[1]

                    elif len(pars)==1:
                        day= 0
                        parName = pars[0]

                    t = datetime.date.today() + datetime.timedelta(days=day)
                    self.setPar( parName,str(t) )

                elif "得到相对时间" == actionName:
                    unit=pars[0]
                    num = int(pars[1])
                    parName = pars[2]

                    now = datetime.datetime.now()

                    if "秒"==unit:
                        t= now+datetime.timedelta(seconds=num)
                    elif "分"==unit:
                        t= now+datetime.timedelta(minutes=num)
                    elif "小时"==unit:
                        t = now+datetime.timedelta(hours=num)
                    elif "天"==unit or "日"==unit:
                        t = now+datetime.timedelta(days=num)
                    elif "星期"==unit or "周"==unit:
                        t = now+datetime.timedelta(weeks=num)
                    timeStr = t.strftime("%Y-%m-%d %H:%M:%S")
                    self.setPar(parName, timeStr)

                elif "得到随机数"==actionName:
                    num = int(pars[0])
                    parName=pars[1]

                    randomStr= self.getRandom(num)
                    self.setPar(parName,randomStr)


                elif "调试输出"==actionName:
                    text = pars[0]
                    print("调试输出\r\n[[[[" + text + "\r\n]]]]")


                elif "点击js弹出框的确定"==actionName:
                    self.alertAccept()

                elif "点击js弹出框的取消"==actionName:
                    self.alertCancel()


                elif "得到子字符串"==actionName:
                    if len(pars) == 4:
                        textSource = pars[0]
                        indexStart =  int(pars[1])
                        indexEnd = int(pars[2])
                        parName = pars[3]
                        subStr = textSource[indexStart:indexEnd]
                        self.setPar(parName,subStr)
                    elif len(pars) == 3:
                        textSource = pars[0]
                        indexStart =  int(pars[1])
                        parName = pars[2]
                        subStr = textSource[indexStart:]
                        self.setPar(parName, subStr)

                elif "替换字符串"==actionName:
                    textSource = pars[0]
                    oldText = pars[1]
                    newText = pars[2]
                    parName = pars[3]
                    tmp =  textSource.replace(oldText,newText)
                    self.setPar(parName, tmp)

                elif "去除字符串"==actionName:
                    textSource = pars[0]
                    oldText = pars[1]
                    parName = pars[2]
                    tmp =  textSource.replace(oldText,"")
                    self.setPar(parName, tmp)


                elif "分割后取第几个"==actionName:
                    textSource = pars[0]
                    splitStr = pars[1]
                    index = int(pars[2])
                    parName = pars[3]
                    tmps = textSource.split(splitStr)
                    tmp= tmps[index]
                    self.setPar(parName, tmp)

                elif "mysql查询"==actionName:
                    ip= pars[0]
                    port = int(  pars[1] )
                    dbName =  pars[2]
                    username= pars[3]
                    password =  pars[4]
                    sql = pars[5]
                    parName = pars[6]
                    text =  MySqlConnect(ip,port,dbName,username,password,self.logger).query(sql)
                    self.setPar(parName, text)





                elif "mysql执行"==actionName:
                    ip = pars[0]
                    port = int(pars[1])
                    dbName = pars[2]
                    username = pars[3]
                    password = pars[4]
                    sql = pars[5]
                    MySqlConnect(ip, port, dbName, username, password, self.logger).execute(sql)

                elif "键盘单键输入"==actionName:
                    key=pars[0]
                    KeyboardAndMouse().keyboardClick(key)


                elif "鼠标点击坐标"==actionName:
                    x=int(pars[0])
                    y = int(pars[1])
                    KeyboardAndMouse().mouseClickLeft(x,y)

                elif "鼠标滚轮"==actionName:
                    value=int(pars[0])*(-1)

                    KeyboardAndMouse().mouseScroll(value)

                elif "鼠标拖拽" == actionName:
                    locates = pars[0]
                    KeyboardAndMouse().mouseDraw(locates)

                elif "屏幕找图并点击"==actionName:
                    picPath=pars[0]
                    KeyboardAndMouse().mouseClickByPic(picPath)

                elif "屏幕找图得到坐标"==actionName:
                    picPath = pars[0]
                    parName0 = pars[1]
                    parName1 = pars[2]
                    parName2 = pars[3]
                    parName3 = pars[4]

                    tmp = KeyboardAndMouse().getPicLocalOnScreen(picPath)
                    self.setPar(parName0,tmp[0])
                    self.setPar(parName1, tmp[1])
                    self.setPar(parName2, tmp[2])
                    self.setPar(parName3, tmp[3])

                    print("左上角x=" + str(tmp[0]))
                    print("左上角y=" + str(tmp[1]))
                    print("右上角x=" + str(tmp[2]))
                    print("右上角y=" + str(tmp[3]))




                elif "屏幕区域截图并保存"==actionName:

                    x0 = int(pars[0])
                    y0 = int(pars[1])
                    x1 = int(pars[2])
                    y1 = int(pars[3])
                    picPath = pars[4]

                    KeyboardAndMouse().saveScreenshotByLocation(x0,y0,x1,y1,picPath)



                elif "识别图片中的字母和数字"==actionName:
                    picPath = pars[0]
                    parName = pars[1]
                    code = IdentifyingCodeDistinguish().distinguish(picPath)
                    self.setPar(parName,code)

                elif "chrome上传单个文件"==actionName:
                    filePath = pars[0]
                    UpLoadWindow().upload(filePath)


                elif "最小化浏览器"==actionName:
                    # self.driver.minimize_window()
                    autoit.win_minimize_all()

                elif "最大化浏览器"==actionName:
                    self.driver.maximize_window()

                elif "设置浏览器位置和大小"==actionName:
                    x = int(pars[0])
                    y = int(pars[1])
                    w = int(pars[2])
                    h = int(pars[3])
                    self.driver.set_window_position(x,y)
                    self.driver.set_window_size(w,h)
                elif "执行命令"==actionName:
                    command = pars[0]
                    parName = pars[1]
                    outText = os.popen(command).read()
                    self.setPar(parName,outText)

                elif "读取多行数据到序列"==actionName:
                    excelPath=pars[0]
                    parName = pars[1]
                    self.getListFromCaseDateFile(excelPath,parName)

                elif "读取电子表格某单元格文本"==actionName:
                    excelPath = pars[0]
                    sheetName= pars[1]
                    row = int(pars[2])     # 电子表格左侧的数字
                    col = int(pars[3])  #电子表格上面的字母对应的数字， A 对应1  B 对应2 ..
                    parName = pars[4]

                    self.getTextOfACell(excelPath,sheetName,row,col,parName)









                elif ""==actionName:
                    pass

                else:
                    raise Exception("单步方法[" + actionName + "]未定义" )



            except Exception as e:
                self.setFailed(e)



    def find(self , idOrXpath,eleName ,timeout=10):
        if "/" in idOrXpath:
            element = EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).find(timeout)
        else:
            element = EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).find(timeout)
        return element


    #休眠
    def sleep(self,second):
        time.sleep(second)

    #获取参数
    def getPar(self,parName):
        return self.pars.get(parName)

    #设置参数
    def setPar(self,parName,parValue):
        self.pars.set(parName,parValue)

    #增加字典
    def addDic(self,dic):
        self.pars.addDic()


    #验证控件存在
    def exist(self, idOrXpath,eleName ,timeout  ):
        if "/" in idOrXpath:
            element =  EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).find(timeout)
        else:
            element = EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).find(timeout)


    def unExist(self, idOrXpath,eleName ,timeout):
        if "/" in idOrXpath:
            EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).waitDisappear(timeout)
        else:
            EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).waitDisappear(timeout)


    def isExist(self, idOrXpath,eleName):

        if "/" in idOrXpath:
            return EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).isExist()
        else:
            return EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).isExist()



    #点击
    def click(self , idOrXpath,eleName):
        if "/" in idOrXpath:
            EleDescription(self.driver,"xpath",idOrXpath,eleName,self.logger).click()
        else:
            EleDescription(self.driver,"id",idOrXpath,eleName,self.logger).click()

    #点击(硬件)
    def click2(self , idOrXpath,eleName):
        if "/" in idOrXpath:
            EleDescription(self.driver,"xpath",idOrXpath,eleName,self.logger).clickByLocate()
        else:
            EleDescription(self.driver,"id",idOrXpath,eleName,self.logger).clickByLocate()

    #点击windows的控件
    def clickWindowsControl(self,windowTitle,controlSysName,controlName):
        autoit.control_click(windowTitle,controlSysName)
        self.logger.appendContent("点击windows的控件["+controlName+"]" )

    #保存控件图像
    def saveElementImg(self,idOrXpath,eleName,imgPath):
        if "/" in idOrXpath:
            EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).saveElementImg(  self.getFileFullPath(imgPath) )
        else:
            EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).saveElementImg(self.getFileFullPath(imgPath))






    #单选按钮和复选框的选中和不选中
    def selectRadioOrCheckbox(self,idOrXpath,eleName,selected):
        ele=None
        if "/" in idOrXpath:
            ele = EleDescription(self.driver,"xpath",idOrXpath,eleName,self.logger).find()
        else:
            ele = EleDescription(self.driver,"id",idOrXpath,eleName,self.logger).find()

        if selected !=  self.driver.execute_script("return arguments[0].checked", ele):
            ele.click()



    #滚动到控件
    def scrollIntoView(self,idOrXpath,eleName):
        if "/" in idOrXpath:
            EleDescription(self.driver,"xpath",idOrXpath,eleName,self.logger).scrollIntoView()
        else:
            EleDescription(self.driver,"id",idOrXpath,eleName,self.logger).scrollIntoView()


    #悬停
    def moveTo(self , idOrXpath,eleName):
        if "/" in idOrXpath:
            EleDescription(self.driver,"xpath",idOrXpath,eleName,self.logger).moveTo()
        else:
            EleDescription(self.driver,"id",idOrXpath,eleName,self.logger).moveTo()


    #双击
    def doubleClick(self ,idOrXpath,eleName):
        if "/" in idOrXpath:
            EleDescription(self.driver,"xpath",idOrXpath,eleName,self.logger).doubleClick()
        else:
            EleDescription(self.driver,"id",idOrXpath,eleName,self.logger).doubleClick()

    # 右键
    def rightClick(self, idOrXpath, eleName):
        if "/" in idOrXpath:
            EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).rightClick()
        else:
            EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).rightClick()


    #拖拽
    def drag(self, idOrXpath,eleName, idOrXpath2,eleName2):

        ele2 = self.find(idOrXpath2,eleName2)

        if "/" in idOrXpath:
            EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).drag(ele2,eleName2)
        else:
            EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).drag(ele2,eleName2)

    #输入
    def input(self,idOrXpath,eleName ,text):
        if "/" in idOrXpath:
            EleDescription(self.driver,"xpath",idOrXpath,eleName,self.logger).input(text)
        else:
            EleDescription(self.driver,"id",idOrXpath,eleName,self.logger).input(text)


    #得到文本
    def getText(self, idOrXpath,eleName ):
        if "/" in idOrXpath:
            return EleDescription(self.driver,"xpath",idOrXpath,eleName,self.logger).text();
        else:
            return EleDescription(self.driver,"id",idOrXpath,eleName,self.logger).text()
    #通过index选择
    def selectByIndex(self ,   idOrXpath,eleName , index):
        if "/" in idOrXpath:
            EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).selectByIndex(index)
        else:
            EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).selectByIndex(index)

    # 通过value选择
    def selectByValue(self ,  idOrXpath,eleName ,value):
        if "/" in idOrXpath:
            EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).selectByValue(value)
        else:
            EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).selectByValue(value)

    # 通过文本选择
    def selectByText(self, idOrXpath,eleName , text):
        if "/" in idOrXpath:
            EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).selectByText(text)
        else:
            EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).selectByText(text)

    #chrome上传文件
    def upload(self,filePath):
        filePath = filePath.replace("/", "\\")
        UpLoadWindow().upload(filePath)

    def alertAccept(self):
        self.driver.switch_to_alert().accept()
        self.logger.appendContent("点击了弹出框的确定按钮")
        self.driver.switch_to.default_content()

    def alertCancel(self):
        self.driver.switch_to_alert().dismiss()
        self.logger.appendContent("点击了弹出框的取消按钮")
        self.driver.switch_to.default_content()

    def getAttr(self, idOrXpath,eleName ,attrName  ):
        if "/" in idOrXpath:
            return EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).getAttr(attrName)
        else:
            return EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).getAttr(attrName)


    def removeAttr(self, idOrXpath,eleName ,attrName ):
        if "/" in idOrXpath:
            EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).removeAttr(attrName)
        else:
            EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).removeAttr(attrName)



    def editAttr(self, idOrXpath,eleName , attrName, attrValue):
        if "/" in idOrXpath:
            EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).editAttr(attrName,attrValue)
        else:
            EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).editAttr(attrName, attrValue)





    def clearText(self,idOrXpath,eleName):
        if "/" in idOrXpath:
            EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).clearText()
        else:
            EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).clearText()



    def getValue(self,idOrXpath,eleName):
        if "/" in idOrXpath:
            return EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).getValue()
        else:
            return EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).getValue()

    def getSelecterTexts(self ,idOrXpath,eleName):
        if "/" in idOrXpath:
            return EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).getSlecterTexts()
        else:
            return EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).getSlecterTexts()


    def isChecked(self,idOrXpath,eleName):
        if "/" in idOrXpath:
            return EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).isChecked()
        else:
            return EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).isChecked()

    # num 位数
    def getRandom(self,num):
        b = 10**num;
        r = random.randint(0,b);
        s= str(r).zfill(num)
        return s


    def switchFrame(self,idOrXpath,eleName):
        if "/" in idOrXpath:
            iframe = EleDescription(self.driver,"xpath",idOrXpath,eleName,self.logger)
        else:
            iframe = EleDescription(self.driver, "id", idOrXpath, eleName, self.logger)
        FrameSwitcher(self.driver,iframe,self.logger).switchFrame()

    def exitFrame(self):
        self.driver.switch_to.default_content()
        self.logger.appendContent("退出frame")


    def switchWindow(self,index):
        WindowSwitcher(self.driver,self.logger).switchWindow(index)

    def closeAllwindowsExceptFirst(self):
        WindowSwitcher(self.driver, self.logger).closeAllwindowsExceptFirst()



    def back(self):
        self.driver.back()

    def forword(self):
        self.driver.forward()

    def refresh(self):
        self.driver.refresh()

    def exeJS(self, jsStr , idOrXpath=None,eleName=None):
        if idOrXpath != None and eleName!=None:

            if "/" in idOrXpath:
                ele = EleDescription(self.driver, "xpath", idOrXpath, eleName, self.logger).find()
            else:
                ele = EleDescription(self.driver, "id", idOrXpath, eleName, self.logger).find()
            self.driver.execute_script( jsStr , ele)
        else:
            self.driver.execute_script(jsStr)

    def getTextOfACell(self, filePath, sheetName, row, col,parName):
        filePath = self.getFileFullPath(filePath)
        text = ExcelOperator().getTextOfACell(filePath,sheetName,row,col)
        self.setPar(parName,text)

    def getListFromCaseDateFile(self,filePath,parName):
        filePath= self.getFileFullPath(filePath)
        maps = ExcelOperator().getDicsFromXls(filePath)
        self.setPar( parName,maps )


    def getFileFullPath(self,filePath):

        filePath = filePath.replace("/", "\\")

        pwd = os.getcwd()
        tmp = pwd.split("src")[0]
        if (filePath[1:2] != ':'):
            # 是相对路径 要到files下面去找

            filesDir = os.path.abspath(tmp + os.path.sep + "..") + "\\files"

            if (not filePath.startswith("\\")):
                filePath = "\\" + filePath
            filePath = filesDir + filePath

        return filePath


if __name__ == "__main__":
    pass
 