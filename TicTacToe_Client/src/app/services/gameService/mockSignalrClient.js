var mockSignalRClient = (function ($) {

    var mock = {};

    //first the items used by unit testing to see what has happened
    mock.callLog = null;
    mock.onFunctionDict = null;
    mock.doneFunc = null;
    mock.failFunc = null;
    mock.errorFunc = null;
    //This logs a string with the caller's function name and the parameters
    //you must provide the function name, but it finds the function arguments itself
    mock.logStep = function (funcName) {
        var log = funcName + '(';
        var callerArgs = arguments.callee.caller.arguments;
        for (var i = 0; i < callerArgs.length; i++) {
            log += (typeof callerArgs[i] === 'function') ? 'function, ' : callerArgs[i] + ', ';
        };
        if (callerArgs.length > 0)
            log = log.substr(0, log.length - 2);
        mock.callLog.push(log + ')');
    };
    mock.reset = function() {
        mock.callLog = [];
        mock.onFunctionDict = {}
        mock.doneFunc = null;
        mock.failFunc = null;
        mock.errorFunc = null;
    };

    //doneFail is the object returned by connection.start()
    var doneFail = {};
    doneFail.done = function (startFunc) {
        mock.logStep('connection.start.done');
        mock.doneFunc = startFunc;
        return doneFail;
    };
    doneFail.fail = function(failFunc) {
        mock.logStep('connection.start.fail');
        mock.failFunc = failFunc;
        return doneFail;
    };

    //Channel is the object returned by connection.createHubProxy
    var channel = {};
    channel.on = function (namedMessage, functionToCall) {
        mock.logStep('channel.on');
        mock.onFunctionDict[namedMessage] = functionToCall;
    };
    channel.invoke = function (actionName, actionGuid) {
        mock.logStep('channel.invoke');
    };

    //connection is the object returned by $.hubConnection
    var connection = {};
    connection.createHubProxy = function (hubName) {
        mock.logStep('connection.createHubProxy');
        return channel;
    };
    connection.error = function (errorFunc) {
        mock.logStep('connection.error');
        mock.errorFunc = errorFunc;
    };
    connection.start = function () {
        mock.logStep('connection.start');
        return doneFail;
    };
    connection.stop = function () {
        mock.logStep('connection.stop');
        return doneFail;
    };

    //now we run once the method to add the hubConnection function to jQuery
    $.hubConnection = function() {
        return connection;
    };

    //Return the mock base which has all the error feedback information in it
    return mock;

}(window.jQuery));

export default mockSignalRClient;