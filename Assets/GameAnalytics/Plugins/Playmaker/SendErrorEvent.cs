#if false

using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("GameAnalytics")]
	[Tooltip("Sends a error event message to the GameAnalytics server.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1171")]
	public class SendErrorEvent : FsmStateAction
	{

		[Tooltip("The severity of this event: critical, error, warning, info, debug")]
		public GA_Error.SeverityType severityType ;
		
		[Tooltip("The event value")]
		public FsmString Message;
		
		[Tooltip("The location of the event")]
		public FsmVector3 Position;
		
		public override void Reset()
		{
			severityType = GA_Error.SeverityType.error;
			Message = new FsmString() { UseVariable = true };
			Position = new FsmVector3() { UseVariable = true };
		}
		
		public override void OnEnter()
		{
			if (Position.IsNone)
			{
				GA.API.Error.NewEvent(
					severityType,
					Message.Value
					);
			}else{
				GA.API.Error.NewEvent(
					severityType,
					Message.Value,
					Position.Value
					);
			}


			Finish();
		}
	}
}

#endif