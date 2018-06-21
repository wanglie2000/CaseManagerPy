import pymysql as MySQLdb


class MySqlConnect(object):
    dbUrl=None
    dbPort=None
    dbName=None
    dbUser=None
    dbPassword=None
    logger =None
    dbCharset='utf8'
    def __init__(self ,dbUrl,dbPort, dbName, dbUser,dbPassword , logger, dbCharset='utf8' ):
        self.dbUrl = dbUrl
        self.dbPort= dbPort
        self.dbName =dbName
        self.dbUser=dbUser
        self.dbPassword=dbPassword
        self.logger = logger
        self.dbCharset=dbCharset

    def execute(self,sql):
        if None!=self.logger:
            self.logger.appendContent("sql=" + sql)

        connect = MySQLdb.Connect(
            host=self.dbUrl,
            port=self.dbPort,
            user=self.dbUser,
            passwd=self.dbPassword,
            db=self.dbName,
            charset=self.dbCharset
        )

        connect.autocommit(True)
        cursor = connect.cursor()
        cursor.execute(sql)

        cursor.close()
        connect.close()




    def query(self,sql):
        if None!=self.logger:
            self.logger.appendContent("sql=" + sql)
        connect = MySQLdb.Connect(
            host= self.dbUrl ,
            port= self.dbPort ,
            user= self.dbUser ,
            passwd= self.dbPassword ,
            db= self.dbName ,
            charset=self.dbCharset
        )
        cursor = connect.cursor()
        cursor.execute(sql)
        text=""
        for row in cursor.fetchall():
            text= text+ str(row) + '\r\n'
        cursor.close()
        connect.close()
        if None!=self.logger:
            self.logger.appendContent("查询结果=\r\n" + text)
        return text


if __name__=="__main__":
    MySqlConnect("127.0.0.1",3306,"casemanager","root","123456",None).execute  ("insert into user(userName,PASSWORD) values('admin3','123456')")










