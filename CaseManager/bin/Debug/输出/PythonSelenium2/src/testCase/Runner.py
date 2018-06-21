# encoding=utf-8

import unittest
from testCase.TmpCase import TmpCase


suite = unittest.TestSuite()
tests = [TmpCase("test")]

suite.addTests(tests)

runner = unittest.TextTestRunner(verbosity=2)
runner.run(suite)

