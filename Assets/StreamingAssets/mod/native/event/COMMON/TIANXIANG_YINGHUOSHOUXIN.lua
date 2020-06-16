occur_days = function ()
    return 10*360
end

options = 
{
    OPTION_1 = 
    {
        selected = function ()
            gm_data().chaoting.prestige = gm_data().chaoting.prestige - 10
        end,

        next_event = function ()
            if gm_is_occur(0.3) then
                return 'CHAOTING_POWER_PARTY_CHANGED'
            end
        end,

        Tooltip = function ()
            return {{'CHAOTING_PRESTIGE_CHANGED', -10}}
        end
    }
}