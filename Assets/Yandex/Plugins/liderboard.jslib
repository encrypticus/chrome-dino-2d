mergeInto(LibraryManager.library, {
  SaveScoreOnLeaderboard: async function (score) {
    try {
      if (window.isScoresLeaderboardAvailable && window.playerMode !== 'lite' && window.leaderboards) {
        window.leaderboards.setLeaderboardScore('scores', score);
      }
    } catch (error) {
      console.log("Ошибка сохранения очков", error);
    }
  }
});
