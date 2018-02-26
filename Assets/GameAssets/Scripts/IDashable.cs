public interface IDashable {
	// Interface for objects dealing with player movement when the player can 'dash' or move faster
	void DashStart ();
	// Sends message to Dashable that player has requested to dash
	void DashStop ();
	// Sends message to Dashable that player has requested to stop dashing

}
