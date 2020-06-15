hide = true

options = 
{
    OPTION_1=
    {
        selected = function ()
            gm_data().taishou.year_score = 'BAD'
        end,

        next_event = function ()
            if gm_data().chaoting.power_party.background ~= gm_data().taishou.background then
                if gm_is_occur(0.1) then
                    return 'EVENT_CHAOTING_REVOKE_TAISHOU_PUBLISH'
                end
            end
            return 'CHAOTING_PIPING_TAISHOU_PUBLISH'
        end,

        tooltip = function ()
            return {{'TAISHOU_YEAR_SCORE_RESULT', 'BAD_SCORE'}}
        end
    }
}