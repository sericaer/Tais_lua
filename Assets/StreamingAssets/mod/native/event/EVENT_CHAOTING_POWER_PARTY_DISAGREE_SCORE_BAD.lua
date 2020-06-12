EVENT_DEF.GLOBAL.EVENT_CHAOTING_POWER_PARTY_DISAGREE_SCORE_BAD = 
{
    options = 
    {
        OPTION_1=
        {
            selected = function ()
                gm_data().taishou.year_score = 'NORMAL'
                gm_data().chaoting.prestige = gm_data().chaoting.prestige - 10
            end,
        }
    }
}