# encoding=utf-8

import xlrd



class ExcelOperator:

    # 第一行作为键   下面的每行作为值
    def getDicsFromXls(self ,filePath):
        eo=  ExcelOperator()
        lists = eo.readExcel(filePath, True)
        dics = eo.getDics(lists)
        return dics



    #第一行作为键   下面的每行作为值
    def getDics(self ,lists):
        h = len(lists)
        dics= [];
        if h>1:


            keyList = lists[0]
            w = len(keyList)
            for y in range(1,h):
                dic={}
                valueList = lists[y]
                for x in range(w):
                    key= keyList[x]
                    value = valueList[x]
                    dic[key]= value
                dics.append(dic)
        return dics


    #根据路径  sheet  行  列  得到文本
    def getTextOfACell(self,filePath,sheetName,row,col):
        tmp = []
        excel = xlrd.open_workbook(filePath)
        table = excel.sheet_by_name(sheetName)
        values = self.unicodeList2StrList(table.row_values(row-1))
        return values[ col-1 ]





    #读取电子表格，返回一个二维的字符list
    #filePath  电子表格文件的路径
    #readFirstRow 是否读取第一行
    def readExcel(self, filePath, readFirstRow):
        tmp = []
        excel = xlrd.open_workbook(filePath)
        table = excel.sheet_by_index(0)
        nrows = table.nrows
        for i in range(nrows):
            #电子表格的第一行，如果需要读取
            if (i == 0):
                if (readFirstRow):
                    # 因为row_values方法读取出来是一个unicodeList，所以需要用方法 unicodeList2StrList 将他转换一下
                    tmp.append(self.unicodeList2StrList(table.row_values(i)))
            else:
                # 因为row_values方法读取出来是一个unicodeList，所以需要用方法 unicodeList2StrList 将他转换一下
                tmp.append(self.unicodeList2StrList(table.row_values(i)))
        return tmp

    #unicode的list转换为字符串list
    def unicodeList2StrList(self, uList):
        # importlib.reload(sys)
        # sys.setdefaultencoding('utf-8')
        tmp = []
        for u in uList:
            print(type(u))

            print(u)
            tmp.append(str(u).strip())
        return tmp











if __name__ == "__main__":
    dics = ExcelOperator().getDicsFromXls("D:\\Book1.xls")


    print(dics)


