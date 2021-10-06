function setupTaskChannel() {
 
    actionRunner.setActionState(actionStates.connectingTransient);
 
    actionRunner.numErrorMessages = 0;
 
    //Setup connection and actionChannel with the functions to call
    var connection = $.hubConnection();
 
    //connection.logging = true;
    actionChannel = connection.createHubProxy('ChatHub');
    setupTaskFunctions();
 
    //Now make sure connection errors are handled
    connection.error(function(error) {
        actionRunner.setActionState(actionStates.failedLink);
        actionRunner.reportSystemError('SignalR error: ' + error);
    });
    //and start the connection and send the start message
    connection.start()
        .done(function() {
            startAction();
        })
        .fail(function(error) {
            actionRunner.setActionState(actionStates.failedConnecting);
            actionRunner.reportSystemError('SignalR connection error: ' + error);
        });
}