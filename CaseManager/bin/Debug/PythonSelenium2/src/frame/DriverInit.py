#encoding=utf-8


from selenium.webdriver.chrome.webdriver import WebDriver as ChromeDriver
from selenium.webdriver.ie.webdriver import WebDriver as InternetExplorerDriver
from selenium.webdriver.firefox.webdriver import WebDriver as FirefoxDriverDriver

from selenium.webdriver import ChromeOptions

class DriverInit(object):
    logger=None
    
    def __init__(self,logger):
        self.logger=logger

    def getWebDriver(self, browser):
        print(browser) 
        
        if browser.upper()=="IE":
            driver= InternetExplorerDriver()
            driver.maximize_window()
            self.logger.appendContent("新建IE驱动")
            return driver
        elif browser.upper()=="CHROME":
            options = ChromeOptions()
            options.add_argument("test-type")
            driver=ChromeDriver(chrome_options=options)
            driver.maximize_window()
            self.logger.appendContent("新建chrome驱动")
            return driver
        elif browser.upper()=="FIREFOX":
            driver= FirefoxDriverDriver()
            driver.maximize_window()
            self.logger.appendContent("新建FireFox驱动")
            return driver
        else:
            return None
 
    
