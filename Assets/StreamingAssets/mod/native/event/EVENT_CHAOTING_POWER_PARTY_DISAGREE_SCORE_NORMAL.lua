EVENT_DEF.GLOBAL.EVENT_CHAOTING_POWER_PARTY_DISAGREE_SCORE_NORMAL = 
{
    options = 
    {
        OPTION_1=
        {
            selected = function ()
                if gm_data().chaoting.power_party.background == gm_data().taishou.background then
                    gm_data().taishou.year_score = 'GOOD'
                end
                if gm_data().chaoting.power_party.background ~= gm_data().taishou.background then
                    gm_data().taishou.year_score = 'BAD'
                end
            end,
        }
    }
}