EVENT_DEF.GLOBAL.EVENT_CHAOTING_POWER_PARTY_DISAGREE_REVOKE = 
{
    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                gm_data().chaoting.prestige = gm_data().chaoting.prestige - 10
                gm_data().chaoting.power_party.prestige = gm_data().chaoting.power_party.prestige -5
            end
        }
    }
}