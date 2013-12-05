@ECHO OFF

REM ################################
REM # setting TOSCA path-variables #
REM ################################

SET XMLPATH=C:/Progra~1/TRICENTIS/TOSCA Testsuite/dll/Settings/XML/MetaSettings.xml


REM #########################
REM # setting AUT variables #
REM #########################

SET AUT_CLASSPATH=D:\Documents\Dropbox\Code\Libs\Java\swt.jar;C:\Program Files (x86)\eclipse\plugins\org.eclipse.core.commands_3.6.100.v20130515-1857.jar;C:\Program Files (x86)\eclipse\plugins\org.eclipse.equinox.common_3.6.200.v20130402-1505.jar;C:\Program Files (x86)\eclipse\plugins\org.eclipse.jface_3.9.0.v20130521-1714.jar;checkTree.jar
REM AUT_CLASSPATH=C:/Programme/eclipse/plugins/org.eclipse.swt_3.3.2.v3347.jar;C:/Programme/eclipse/plugins/org.eclipse.swt.win32.win32.x86_3.3.2.v3347a.jar;C:/Programme/eclipse/plugins/org.eclipse.ui.forms_3.3.0.v20070511.jar;SWTTest.jar
SET AUT_MAIN_CLASS="CheckFileTree"

REM ##############################
REM # setting classpath-variable #
REM ##############################

SET CLASSPATH=%AUT_CLASSPATH%

ECHO %classpath%

REM ###################################
REM # setting java runtime parameters #
REM ###################################

SET JVM_PARAMETERS=-Djava.library.path="C:/Windows/System32;C:/Progra~2/eclipse/plugins/" -Dtoscaautclasspath="%AUT_CLASSPATH%" -Dtoscaautclassname=%AUT_MAIN_CLASS%
REM # append the following parameters (without "") to above line to enable debugging-capability: " -Xdebug -Xrunjdwp:transport=dt_socket,server=y,address=8000,suspend=n"

REM ##########################
REM # begin startup sequence #
REM ##########################

REM ECHO Starting rmiregistry.exe ...
REM START /MIN RMIREGISTRY

ECHO Starting server ...
java %JVM_PARAMETERS% at.tosca.javaengine.starter.BatchStarter
	
PAUSE