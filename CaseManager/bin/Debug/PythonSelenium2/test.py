#coding=utf-8

import autoit

from PIL import ImageGrab


# autoit.run("notepad.exe")
# autoit.win_wait_active("[CLASS:Notepad]", 3)
# autoit.control_send("[CLASS:Notepad]", "Edit1", "hello world{!}")
# autoit.win_close("[CLASS:Notepad]")
# autoit.control_click("[Class:#32770]", "Button2")

# autoit.win_move("无标题 - 记事本",10,10)
#
im = ImageGrab.grab(( 100,100,200,200 ))

im.save( "xx.png" ,'png')


