function upgrade() {
	        alert('Please use Google Chrome for best experience');
	      }
function record (id) {
		  //var id = $(this).attr('id');
	  console.log(id)
	if (!(window.webkitSpeechRecognition) && !(window.speechRecognition)) {
	  upgrade();
	} else {
	  var recognizing;
	  transcription = document.getElementById("speech_"+id);
	  var speech = new webkitSpeechRecognition() || speechRecognition();
	  speech.continuous = true;
	  speech.interimResults = true;
	  speech.lang = 'vi-VN'; // check google web speech example source for more lanuages
	  speech.start(); //enables recognition on default
	  speech.onstart = function() {
	      // When recognition begins
	      recognizing = true;
	  };
	  speech.onresult = function(event) {
	    // When recognition produces result
	var interim_transcript = '';
	var final_transcript = '';
	// main for loop for final and interim results
	    for (var i = event.resultIndex; i < event.results.length; ++i) {
	      if (event.results[i].isFinal) {
	        final_transcript += event.results[i][0].transcript;
	      } else {
	        interim_transcript += event.results[i][0].transcript;
	      }
	    }
	    transcription.value += final_transcript;
	    
	  };
	  speech.onerror = function(event) {
	      // Either 'No-speech' or 'Network connection error'
	      console.error(event.error);
	  };
	  speech.onend = function(event){
	  	console.log("Push Record button to start");
	      }
	    }
	  };