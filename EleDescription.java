package base;

import java.util.Date;
import java.util.List;

import org.openqa.selenium.By;
import org.openqa.selenium.Dimension;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.Keys;
import org.openqa.selenium.Point;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.support.ui.Select;
import org.sikuli.script.Match;
import org.sikuli.script.Screen;

public class EleDescription
{
	WebDriver driver;
	String findType;
	String findValue;
	String eleName;
	Logger logger;
	
	/*
	int xOffset;
	int yOffset;
	*/
	
	String mouseStr= "<div id='mouse' style='width: 22px; height: 22px; position: absolute; top: TOPpx; left: LEFTpx;'><div style='width: 20px; height: 1px; background-color: #000000; top: 0px; left: 0px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 19px; height: 1px; background-color: #000000; top: 1px; left: 0px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 18px; height: 1px; background-color: #FFFFFF; top: 2px; left: 1px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 17px; height: 1px; background-color: #FFFFFF; top: 3px; left: 1px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 16px; height: 1px; background-color: #FFFFFF; top: 4px; left: 2px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 15px; height: 1px; background-color: #FFFFFF; top: 5px; left: 2px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 14px; height: 1px; background-color: #FFFFFF; top: 6px; left: 3px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 13px; height: 1px; background-color: #FFFFFF; top: 7px; left: 3px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 12px; height: 1px; background-color: #FFFFFF; top: 8px; left: 4px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 11px; height: 1px; background-color: #FFFFFF; top: 9px; left: 4px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 10px; height: 1px; background-color: #FFFFFF; top: 10px; left: 5px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 9px; height: 1px; background-color: #FFFFFF; top: 11px; left: 5px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 8px; height: 1px; background-color: #FFFFFF; top: 12px; left: 6px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 7px; height: 1px; background-color: #FFFFFF; top: 13px; left: 6px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 6px; height: 1px; background-color: #FFFFFF; top: 14px; left: 7px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 5px; height: 1px; background-color: #FFFFFF; top: 15px; left: 7px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 4px; height: 1px; background-color: #FFFFFF; top: 16px; left: 8px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 3px; height: 1px; background-color: #FFFFFF; top: 17px; left: 8px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 2px; height: 1px; background-color: #FFFFFF; top: 18px; left: 9px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div><div style='width: 1px; height: 1px; background-color: #FFFFFF; top: 19px; left: 9px; position: absolute; border-left-width: 3px; border-left-style: solid; border-left-color: #000000;'></div></div>";
	
	
	
	
	
	public Point getElementScreenLacation(WebElement ele) throws Exception
	{
		if(null != ele )
		{
			Point p = ele.getLocation();
			Dimension d = ele.getSize();
			
			int x = p.x + d.width/2 ;
			int y = p.y + d.height/2;
			
			String js01="if(null!=document.getElementById(\"elementmark\"))\r\n";
			String js02="{\r\n";
			
			String js03= "document.body.removeChild(document.getElementById(\"elementmark\"));\r\n";
			String js04="}\r\n";
			String js05 = "var div= document.createElement(\"div\");\r\n"; 
			String js06 =  "document.body.appendChild(div);\r\n";
			String js07 =  "div.id=\"elementmark\";\r\n";
			String js08 =  "div.setAttribute(\"style\",\"z-index:999; position: absolute; width: 4px; height: 4px; top: " + y + "px; left: " + x + "px\");\r\n";
			String js09 =  "div.innerHTML=\"<div style='background-color: #FFFFFF; position: absolute; width: 1px; height: 1px; top: 0px; left: 0px;'></div><div style='background-color: #FFFF00; position: absolute; width: 1px; height: 1px; top: 0px; left: 1px;'></div><div style='background-color: #00FFFF; position: absolute; width: 1px; height: 1px; top: 0px; left: 2px;'></div><div style='background-color: #00FF00; position: absolute; width: 1px; height: 1px; top: 0px; left: 3px;'></div><div style='background-color: #FF00FF; position: absolute; width: 1px; height: 1px; top: 1px; left: 0px;'></div><div style='background-color: #FF0000; position: absolute; width: 1px; height: 1px; top: 1px; left: 1px;'></div><div style='background-color: #0000FF; position: absolute; width: 1px; height: 1px; top: 1px; left: 2px;'></div><div style='background-color: #000000; position: absolute; width: 1px; height: 1px; top: 1px; left: 3px;'></div><div style='background-color: #FFFFFF; position: absolute; width: 1px; height: 1px; top: 2px; left: 0px;'></div><div style='background-color: #FFFF00; position: absolute; width: 1px; height: 1px; top: 2px; left: 1px;'></div><div style='background-color: #00FFFF; position: absolute; width: 1px; height: 1px; top: 2px; left: 2px;'></div><div style='background-color: #00FF00; position: absolute; width: 1px; height: 1px; top: 2px; left: 3px;'></div><div style='background-color: #FF00FF; position: absolute; width: 1px; height: 1px; top: 3px; left: 0px;'></div><div style='background-color: #FF0000; position: absolute; width: 1px; height: 1px; top: 3px; left: 1px;'></div><div style='background-color: #0000FF; position: absolute; width: 1px; height: 1px; top: 3px; left: 2px;'></div><div style='background-color: #000000; position: absolute; width: 1px; height: 1px; top: 3px; left: 3px;'></div>\";";
			
			String jsStr = new StringBuffer().append(js01).append(js02).append(js03).append(js04).append(js05).append(js06).append(js07).append(js08).append(js09).toString();
		
			((JavascriptExecutor)(this.driver)).executeScript( jsStr );
			
		
			Screen screen = new Screen();
			Match match = screen.exists("8c.png") ;
			
			System.out.println( match.x + "---------" + match.y );
			
			jsStr = new StringBuffer().append(js01).append(js02).append(js03).append(js04).toString();
			((JavascriptExecutor)(this.driver)).executeScript( jsStr );
			
			
			return new Point(match.x,  match.y); 
		}
		else
		{
			return new Point(-1, -1);
		}
		
		
	}
	
	
	
	/*
	private void showMouse(WebElement ele)
	{
		Point p =  ele.getLocation();
		Dimension d = ele.getSize();
		int x = p.x + d.width/2 +1;
		int y = p.y + d.height/2 + 1 ;
		
		String addHtml = mouseStr.replaceAll("LEFT", ""+x).replaceAll("TOP", ""+y);
		String javascriptStr = "$('#mouse').remove();";
		javascriptStr += "$('body').append(\"" + addHtml + "\");" ;
		((JavascriptExecutor)(this.driver)).executeScript(javascriptStr);
		
	}
	*/

	private void xxx(WebElement ele)
	{
		/*
		 * String jsStr= new TextProcessor().getAllTxtFromFile("js\\xxx.js");
		 * System.out.println(jsStr);
		 * ((JavascriptExecutor)(this.driver)).executeScript(jsStr, ele);
		 */

	}
	/*
	public EleDescription(WebDriver driver, String findType, String findValue, String eleName, Logger logger ,int xOffset,int yOffset)
	{
		this.driver = driver;
		this.findType = findType;
		this.findValue = findValue;
		this.eleName = eleName;
		this.logger = logger;
		this.xOffset = xOffset;
		this.yOffset = yOffset;
		
	}
	*/
	
	
	

	public EleDescription(WebDriver driver, String findType, String findValue, String eleName, Logger logger)
	{
		this.driver = driver;
		this.findType = findType;
		this.findValue = findValue;
		this.eleName = eleName;
		this.logger = logger;
	}

	/**
	 * 得到这个控件是兄弟中的第几个 从0开始算
	 * 
	 * @return
	 */
	public int getIndex()
	{
		WebElement ele = find(10);
		if (null != ele)
		{
			List<WebElement> brothers = ele.findElements(By.xpath("./../*"));
			int l = brothers.size();
			for (int i = 0; i < l; i++)
			{
				if (brothers.get(i).equals(ele))
				{
					logger.appendContent("得到控件[" + this.eleName + "]的序号为[" + i + "]");
					return i;
				}
			}
			logger.appendContent("得到控件[" + this.eleName + "]的序号失败");
			return -1;
		}
		else
		{
			logger.appendContent("得到控件[" + this.eleName + "]的序号失败");
			return -1;
		}
	}

	/**
	 * 得到input类型的控件上已经输入的文本
	 * 
	 * @return
	 */
	public String getValue()
	{
		WebElement ele = find(10);

		if (null != ele)
		{
			String value =  ((JavascriptExecutor) driver).executeScript("return arguments[0].value", ele).toString() ;
			logger.appendContent("得到控件[" + this.eleName + "]的value为[" + value + "]");

			return value;
		}
		else
		{
			logger.appendContent("获取控件[" + this.eleName + "]的value失败");
			return null;
		}
	}

	/**
	 * 查找控件
	 * 
	 * @param timeout
	 * @return
	 */
	public WebElement find(int timeout)
	{
		long startTime = new Date().getTime();
		long endTime = startTime + timeout * 1000;

		if ("id".equalsIgnoreCase(findType))
		{
			while (this.driver.findElements(By.id(findValue)).size() == 0 && new Date().getTime() < endTime)
			{
				try
				{
					Thread.sleep(200);
				}
				catch (Exception e)
				{
					// TODO: handle exception
				}
			}
			List<WebElement> list = this.driver.findElements(By.id(findValue));

			if (list.size() > 0)
			{
				logger.appendContent("根据id[" + this.findValue + "]找到控件[" + this.eleName + "]");
				((JavascriptExecutor) driver).executeScript("arguments[0].style.border='2px solid red'", list.get(0));
				// this.xxx(list.get(0));
				return list.get(0);
			}
			else
			{
				logger.appendContent("根据id[" + this.findValue + "]没有找到控件[" + this.eleName + "]");
				return null;
			}

		}
		else if ("xpath".equalsIgnoreCase(findType))
		{

			while (this.driver.findElements(By.xpath(findValue)).size() == 0 && new Date().getTime() < endTime)
			{
				try
				{
					Thread.sleep(200);
				}
				catch (Exception e)
				{
					// TODO: handle exception
				}
			}
			List<WebElement> list = this.driver.findElements(By.xpath(findValue));

			if (list.size() > 0)
			{
				logger.appendContent("根据xpath[" + this.findValue + "]找到控件[" + this.eleName + "]");
				((JavascriptExecutor) driver).executeScript("arguments[0].style.border='2px solid red'", list.get(0));

				// this.xxx(list.get(0));
				return list.get(0);
			}
			else
			{
				logger.appendContent("根据xpath[" + this.findValue + "]没有找到控件[" + this.eleName + "]");
				return null;
			}
		}

		return null;
	}
	
	/**
	 * 是否存在并且是显示的状态
	 * @return
	 */
	public boolean isExistAndShow()
	{
		WebElement ele = find(10);
		if( null != ele )
		{
			String displayStr = ((JavascriptExecutor)(this.driver)).executeScript("return arguments[0].style.display").toString();
			return !(displayStr.equalsIgnoreCase("none"));

		}
		else
		{
			return false;
		}
		
	}
	
	/**
	 * 是否不存在或者隐藏
	 * @return
	 */
	public boolean isNotExistOrHide()
	{
		WebElement ele = find(10);
		if(null != ele)
		{
			String displayStr = ((JavascriptExecutor)(this.driver)).executeScript("return arguments[0].style.display").toString();
			return (displayStr.equalsIgnoreCase("none"));
		}
		else
		{
			return true;
		}
		
	}
	
	

	/**
	 * 查找控件
	 * 
	 * @return
	 */
	public WebElement find()
	{
		return find(10);
	}
	/*
	//通过真实的鼠标移动点击
	public void clickByActualMouse() throws FindFailed, IOException
	{
		WebElement ele = find(10);
		String eleImg= "tmp.png";
		this.getElementImg(this.driver, ele, eleImg);
		Screen screen = new Screen();
		screen.click(eleImg);
	}
	*/
	
	
	
	
	

	// 单击
	public void click() throws Exception
	{
		
		
		WebElement ele = find(10);

	//	this.showMouse(ele);

		ele.click();
		logger.appendContent("单击控件[" + this.eleName + "]成功");
		System.out.println("单击控件[" + this.eleName + "]成功");

	}

	/**
	 * 通过actions的方式点击，有时候通过普通的点击可能完成不了，就用这种点击
	 */
	public void clickByActions()
	{
		WebElement ele = find(10);

		Actions actions = new Actions(driver);
		actions.click(ele).perform();
		logger.appendContent("actions单击控件[" + this.eleName + "]成功");
	}
	
	/**
	 * 通过js的方式点击
	 */
	
	public void clickByJs()
	{
		WebElement ele = find(10);
		((JavascriptExecutor)(this.driver)).executeScript("arguments[0].click()", ele);
	}
	
	
	public void inputEnter() throws Exception
	{
		WebElement ele = find(10);
		ele.sendKeys(Keys.ENTER);
		logger.appendContent("在控件[" + this.eleName + "]中输入回车成功");
	}

	// 输入
	public void input(String text) throws Exception
	{
		WebElement ele = find(10);
	
		ele.clear();
		ele.sendKeys(text);
//		Point p= this.getElementScreenLacation(ele);
		logger.appendContent("在控件[" + this.eleName + "]中输入文本[" + text + "]成功");
	}

	// 输入 但是通过js直接控制value实现的
	public void inputByJs(String text)
	{
		WebElement ele = find(10);

		((JavascriptExecutor) driver).executeScript("arguments[0].value='" + text + "'", ele);

		logger.appendContent("在控件[" + this.eleName + "]中通过Js输入文本[" + text + "]成功");

	}

	// 将控件去除
	public void remove()
	{
		WebElement ele = find(10);
		((JavascriptExecutor) driver).executeScript("arguments[0].removeNode();", ele);
		logger.appendContent("移除控件[" + this.eleName + "]成功");
	}

	public void scrollIntoView()
	{
		WebElement ele = find(10);
		((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView();", ele);
		logger.appendContent("根据控件[" + this.eleName + "]控制滚动条成功");
	}

	public void doubleClick()
	{
		WebElement ele = find(10);

		Actions actions = new Actions(driver);
		actions.doubleClick(ele).perform();
		logger.appendContent("双击控件[" + this.eleName + "]成功");
	}
	
	public void rightClick()
	{
		WebElement ele = find(10);

		Actions actions = new Actions(driver);
		actions.contextClick(ele).perform();
		logger.appendContent("右键单击控件[" + this.eleName + "]成功");
		
		
	}
	

	/**
	 * 拖拽
	 * 
	 * @param eleDes2
	 *            拖拽到这个控件这里
	 */
	public void drag(EleDescription eleDes2)
	{
		WebElement ele = find(10);
		WebElement ele2 = eleDes2.find(10);

		Actions actions = new Actions(driver);
		actions.dragAndDrop(ele, ele2).perform();
		logger.appendContent("从控件[" + this.eleName + "]拖拽到控件[" + eleDes2.eleName + "]成功");

	}

	/**
	 * 鼠标移动到这个控件
	 */
	public void moveTo()
	{
		WebElement ele = find(10);
		Actions actions = (Actions) driver;
		actions.moveToElement(ele).perform();
		logger.appendContent("鼠标移动到控件[" + this.eleName + "]成功");

	}

	/**
	 * 下拉控件 根据文本选
	 * 
	 * @param text
	 */
	public void selectByText(String text)
	{
		WebElement ele = find(10);
		Select select = new Select(ele);
		select.selectByVisibleText(text);
		logger.appendContent("控件[" + this.eleName + "]根据文本[" + text + "]选择成功");

	}

	/**
	 * 下拉控件 根据value选
	 * 
	 * @param value
	 */
	public void selectByValue(String value)
	{
		WebElement ele = find(10);
		Select select = new Select(ele);
		select.selectByValue(value);
		logger.appendContent("控件[" + this.eleName + "]根据值[" + value + "]选择成功");

	}

	/**
	 * 下拉控件 根据 index选
	 * 
	 * @param index
	 */
	public void selectByIndex(int index)
	{
		WebElement ele = find(10);
		Select select = new Select(ele);
		select.selectByIndex(index);
		logger.appendContent("控件[" + this.eleName + "]根据序号[" + index + "]选择成功");

	}

	/**
	 * 得到控件的文本
	 * 
	 * @return
	 */
	public String getText()
	{
		WebElement ele = find(10);
		//String text = ele.getText();
		String text = ((JavascriptExecutor)(this.driver)).executeScript("return arguments[0].innerText", ele).toString() ;
		
		logger.appendContent("得到控件[" + this.eleName + "]的文本为[" + text + "]");
		return text;
	}

	/**
	 * 去除控件的某个属性
	 * 
	 * @param attributeName
	 */
	public void removeAttribute(String attributeName)
	{
		WebElement ele = find(10);
		((JavascriptExecutor) driver).executeScript("arguments[0].removeAttribute('" + attributeName + "')", ele);

		logger.appendContent("去除控件[" + this.eleName + "]的属性[" + attributeName + "]成功");
	}

	/**
	 * 设置控件的某个属性
	 * 
	 * @param attributeName
	 * @param attributeValue
	 */
	public void setAttribute(String attributeName, String attributeValue)
	{
		WebElement ele = find(10);
		((JavascriptExecutor) driver).executeScript("arguments[0].setAttribute('" + attributeName + "','" + attributeValue + "')", ele);
		logger.appendContent("修改控件[" + this.eleName + "]的属性[" + attributeName + "]为[" + attributeValue + "]成功");
	}

	/**
	 * 将隐藏的控件显示出来
	 */
	public void show()
	{
		WebElement ele = find(10);
		((JavascriptExecutor) driver).executeScript("arguments[0].style.display='block'", ele);
		logger.appendContent("将控件[" + this.eleName + "]由隐藏设置为显示");
	}

	/**
	 * 将控件隐藏
	 */
	public void hide()
	{
		WebElement ele = find(10);
		((JavascriptExecutor) driver).executeScript("arguments[0].style.display='none'", ele);
		logger.appendContent("将控件[" + this.eleName + "]由显示设置为隐藏");

	}

	/**
	 * 将只读的控件变为可控
	 */
	public void removeReadOnly()
	{
		WebElement ele = find(10);
		((JavascriptExecutor) driver).executeScript("arguments[0].readOnly=false", ele);
		logger.appendContent("将控件[" + this.eleName + "]的只读属性去掉");
	}

}
