#encoding=utf-8


import pyautogui
import time

time.sleep(2)

pyautogui.moveTo(300, 300, duration=0.25)
pyautogui.mouseDown(300,300)
pyautogui.moveTo(400, 300, duration=0.25)
pyautogui.moveTo(400, 400, duration=0.25)
pyautogui.moveTo(300, 400, duration=0.25)

pyautogui.moveTo(300, 300, duration=0.25)

pyautogui.mouseUp(300,300)

pyautogui.click(300,300)
pyautogui.typewrite("abcde")

pyautogui.typewrite(["enter"])
