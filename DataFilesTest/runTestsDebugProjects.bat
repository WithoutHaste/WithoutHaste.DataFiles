REM remove everything from the logs folder so new results can be saved
set folder="logs"
cd /d %folder%
for /F "delims=" %%i in ('dir /b') do (rmdir "%%i" /s/q || del "%%i" /s/q)
cd ..
REM run tests
mstest /testcontainer:bin\Debug\DataFilesTest.net20.dll /resultsfile:logs\DataFilesTestOutput.net20.Debug.trx
mstest /testcontainer:bin\Debug\DataFilesTest.net30.dll /resultsfile:logs\DataFilesTestOutput.net30.Debug.trx
mstest /testcontainer:bin\Debug\DataFilesTest.net35.dll /resultsfile:logs\DataFilesTestOutput.net35.Debug.trx
mstest /testcontainer:bin\Debug\DataFilesTest.net40.dll /resultsfile:logs\DataFilesTestOutput.net40.Debug.trx
mstest /testcontainer:bin\Debug\DataFilesTest.net45.dll /resultsfile:logs\DataFilesTestOutput.net45.Debug.trx
mstest /testcontainer:bin\Debug\DataFilesTest.net451.dll /resultsfile:logs\DataFilesTestOutput.net451.Debug.trx
mstest /testcontainer:bin\Debug\DataFilesTest.net452.dll /resultsfile:logs\DataFilesTestOutput.net452.Debug.trx
mstest /testcontainer:bin\Debug\DataFilesTest.net46.dll /resultsfile:logs\DataFilesTestOutput.net46.Debug.trx
mstest /testcontainer:bin\Debug\DataFilesTest.net461.dll /resultsfile:logs\DataFilesTestOutput.net461.Debug.trx
mstest /testcontainer:bin\Debug\DataFilesTest.net462.dll /resultsfile:logs\DataFilesTestOutput.net462.Debug.trx
mstest /testcontainer:bin\Debug\DataFilesTest.net47.dll /resultsfile:logs\DataFilesTestOutput.net47.Debug.trx
mstest /testcontainer:bin\Debug\DataFilesTest.net471.dll /resultsfile:logs\DataFilesTestOutput.net471.Debug.trx
mstest /testcontainer:bin\Debug\DataFilesTest.net472.dll /resultsfile:logs\DataFilesTestOutput.net472.Debug.trx
